using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : BaseUpdatable
{
    private IInventory inventory;
    private IGatherManager gatherManager;

    private readonly List<CraftingRecipe> lockedRecipes;

    private readonly List<CraftingQueueObject> craftingQueue;
    private readonly List<UnlockedRecipeObject> unlockedRecipes;
    private readonly Image craftingQueueProgressSlider;

    private readonly GameObject recipeUIPrefab;
    private readonly RectTransform recipeUIParent;
    private readonly GameObject craftingQueueItemUIPrefab;
    private readonly RectTransform craftingQueueUIParent;

    private readonly Sprite check;
    private readonly Sprite cross;

    private readonly RecipeToolTipObject toolTip;

    public CraftingManager(RectTransform _recipeUIParent, RectTransform _craftingQueueUIParent, RectTransform _toolTipTransform, Image _craftingQueueProgressSlider)
    {
        toolTip = new RecipeToolTipObject(_toolTipTransform);
        ServiceLocator.Instance.Get<ITickManager>().Add(toolTip);

        check = Resources.Load<Sprite>("Art/UI_Flat_Checkmark_Medium");
        cross = Resources.Load<Sprite>("Art/UI_Flat_Cross_Medium");

        recipeUIPrefab = Resources.Load<GameObject>("Crafting/Recipe");
        recipeUIParent = _recipeUIParent;

        craftingQueueItemUIPrefab = Resources.Load<GameObject>("Crafting/CraftingQueueItem");
        craftingQueueUIParent = _craftingQueueUIParent;

        craftingQueueProgressSlider = _craftingQueueProgressSlider;

        craftingQueue = new List<CraftingQueueObject>();

        lockedRecipes = new List<CraftingRecipe>();
        unlockedRecipes = new List<UnlockedRecipeObject>();

        CraftingRecipe[] allRecipes = Resources.LoadAll<CraftingRecipe>("Data/CraftingRecipes");
        lockedRecipes.AddRange(allRecipes);
    }

    public override void OnStart()
    {
        inventory = ServiceLocator.Instance.Get<IInventory>();
        gatherManager = ServiceLocator.Instance.Get<IGatherManager>();
    }

    public override void OnFixedUpdate()
    {
        CheckForRecipeUnlocks();
        UpdateCraftingQueue();
        UpdateUnlockedRecipes();
    }

    public void QueueCraft(CraftingRecipe _recipe)
    {
        if (!CanBeCrafted(_recipe)) { return; }

        inventory.Remove(_recipe.Input);
        craftingQueue.Add(new CraftingQueueObject(_recipe, craftingQueueUIParent, craftingQueueItemUIPrefab));
    }

    private bool CanBeCrafted(CraftingRecipe _recipe)
    {
        return inventory.Contains(_recipe.Input);
    }

    private void CheckForRecipeUnlocks()
    {
        for (int recipeIndex = lockedRecipes.Count - 1; recipeIndex >= 0; recipeIndex--)
        {
            CraftingRecipe lockedRecipe = lockedRecipes[recipeIndex];
            ItemStack[] itemStack = (ItemStack[])lockedRecipe.Input.Clone();

            for (int stackIndex = itemStack.Length - 1; stackIndex >= 0; stackIndex--)
            {
                itemStack[stackIndex].Amount = 1;
            }

            if (inventory.Contains(itemStack))
            {
                lockedRecipes.RemoveAt(recipeIndex);
                unlockedRecipes.Add(
                    new UnlockedRecipeObject(lockedRecipe, recipeUIParent, recipeUIPrefab, () => QueueCraft(lockedRecipe), toolTip, check, cross)
                );
            }
        }
    }

    private void UpdateCraftingQueue()
    {
        if (craftingQueue.Count <= 0) { return; }
        
        CraftingQueueObject currentItem = craftingQueue[0];
        currentItem.PassedTime += Time.deltaTime;
        craftingQueueProgressSlider.fillAmount = Mathf.InverseLerp(0, currentItem.Recipe.Duration, currentItem.PassedTime);

        if (currentItem.PassedTime > currentItem.Recipe.Duration)
        {
            inventory.Add(currentItem.Recipe.Output);

            if (currentItem.Recipe.GatherComponet != null) { gatherManager.AddGatherComponent(currentItem.Recipe.GatherComponet); }

            currentItem.Destroy();
            craftingQueue.RemoveAt(0);
        }
    }

    private void UpdateUnlockedRecipes()
    {
        foreach (UnlockedRecipeObject recipeObject in unlockedRecipes) 
        {
            if (CanBeCrafted(recipeObject.Recipe)) { recipeObject.SetIsCraftable(true); }
            else { recipeObject.SetIsCraftable(false); }
        }
    }
}
