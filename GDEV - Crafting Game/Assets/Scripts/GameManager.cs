using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Crafting System")]
    [SerializeField] private CraftingRecipe[] allAvailableRecipes;
    [SerializeField] private GameObject recipeUIPrefab;
    [SerializeField] private RectTransform recipeUIParent;
    
    [Header("Reference")]
    [SerializeField] private RectTransform inventoryRoot;

    private ServiceLocator serviceLocator = null;
    private InventoryManager inventoryManager = null;
    private TickManager tickManager = null;
    private InteractionManager interactionManager = null;

    private void Awake()
    {
        serviceLocator = new ServiceLocator();

        tickManager = new TickManager();
        interactionManager = new InteractionManager();
        inventoryManager = new InventoryManager(inventoryRoot);

        serviceLocator.Add(tickManager, typeof(ITickManager));
        serviceLocator.Add(inventoryManager.Inventory, typeof(IInventory));
      //serviceLocator.Add(interactionManager); Switched to the constructor of InteractionManager class!

        tickManager.Add(new GatherManager(
            new Dictionary<ItemData, GatherInfo.GatherChance>()
            {
                // Insert Standard ItemData
            }
        ));

        tickManager.Add(interactionManager);
        //tickManager.Add(new CraftingManager(allAvailableRecipes, recipeUIPrefab, recipeUIParent));
    }

    private void Start()
    {
        tickManager.OnStart();
    }

    private void Update()
    {
        tickManager.OnUpdate();
    }

    private void FixedUpdate()
    {
        tickManager.OnFixedUpdate();
    }
}
