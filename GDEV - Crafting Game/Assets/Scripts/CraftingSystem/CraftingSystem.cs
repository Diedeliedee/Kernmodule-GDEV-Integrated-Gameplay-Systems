using UnityEngine;

public class CraftingSystem : IUpdatable
{
    private IInventory inventory;
    private CraftingRecipe[] recipes;

    public void OnStart() 
    {
        inventory = ServiceLocator.Instance.Get<IInventory>();
        recipes = (CraftingRecipe[])Resources.FindObjectsOfTypeAll(typeof(CraftingRecipe));
    }
    
    public void OnFixedUpdate() 
    { 
        
    }

    public void OnUpdate() { }
}
