using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Crafting System")]
    [SerializeField] private CraftingRecipe[] allAvailableRecipes;
    [SerializeField] private GameObject recipeUIPrefab;
    [SerializeField] private RectTransform recipeUIParent;
    
    private ServiceLocator serviceLocator;
    private TickManager tickManager;
    private Inventory inventory;

    private void Awake()
    {
        tickManager = new TickManager();
        inventory = new Inventory();
        serviceLocator = new ServiceLocator();
        serviceLocator.Add(typeof(IInventory), inventory);
        serviceLocator.Add(typeof(ITickManager), tickManager);

        tickManager.Add(new GatherSystem(
            new Dictionary<ItemData, GatherInfo.GatherChance>()
            {
                // Insert Standard ItemData
            }
        ));

        tickManager.Add(new CraftingSystem(allAvailableRecipes, recipeUIPrefab, recipeUIParent));
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
