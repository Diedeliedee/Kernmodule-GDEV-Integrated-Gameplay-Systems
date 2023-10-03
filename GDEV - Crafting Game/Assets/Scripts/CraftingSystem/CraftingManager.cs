using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : IUpdatable
{
    private IInventory inventory;
    private List<CraftingRecipe> lockedRecipes;
    private List<CraftingRecipe> unlockedRecipes;

    private Queue<CraftingRecipe> recipeQueue;

    private GameObject recipeUIPrefab;
    private RectTransform recipeUIParent;

    public CraftingManager(CraftingRecipe[] _allRecipes, GameObject _recipeUIPrefab, RectTransform _recipeUIParent)
    {
        recipeUIPrefab = _recipeUIPrefab;
        recipeUIParent = _recipeUIParent;

        lockedRecipes = new List<CraftingRecipe>();
        unlockedRecipes = new List<CraftingRecipe>();

        lockedRecipes.AddRange(_allRecipes);
    }

    public void OnStart() 
    {
        inventory = ServiceLocator.Instance.Get<IInventory>();
    }
    
    public void OnFixedUpdate()
    {
        CheckForRecipeUnlocks();
    }

    public void OnUpdate() { }


    public bool CanBeCrafted(CraftingRecipe recipe)
    {
        return inventory.Contains(recipe.Input);
    }

    public void QueueCraft(CraftingRecipe recipe)
    {
        if (!CanBeCrafted(recipe)) { return; }

        recipeQueue.Enqueue(recipe);
    }

    private void Craft(CraftingRecipe recipe)
    {
        if (!CanBeCrafted(recipe)) { return; }

        inventory.Remove(recipe.Input);
        inventory.Add(recipe.Output);
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
                unlockedRecipes.Add(lockedRecipe);

                GameObject newRecipe = Object.Instantiate(recipeUIPrefab, recipeUIParent);
                newRecipe.GetComponentInChildren<TextMeshProUGUI>().text = lockedRecipe.name;
                newRecipe.GetComponentInChildren<Button>().onClick.AddListener(() => Craft(lockedRecipe));
            }
        }
    }
}
