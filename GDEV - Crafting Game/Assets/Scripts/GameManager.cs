using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Gather System")]
    [SerializeField] private GatherComponent baseGatherComponent;

    [Header("Crafting System")]
    [SerializeField] private CraftingRecipe[] allAvailableRecipes;
    [SerializeField] private GameObject recipeUIPrefab;
    [SerializeField] private RectTransform recipeUIParent;
    
    [Header("Reference")]
    [SerializeField] private RectTransform canvas;

    private ServiceLocator serviceLocator = null;
    private InventoryManager inventoryManager = null;
    private TickManager tickManager = null;

    private void Awake()
    {
        tickManager = new TickManager();
        inventoryManager = new InventoryManager(canvas);

        serviceLocator = new ServiceLocator();
        serviceLocator.Add(tickManager, typeof(ITickManager));
        serviceLocator.Add(inventoryManager.Inventory, typeof(IInventory));

        tickManager.Add(new GatherManager(baseGatherComponent));
        tickManager.Add(
            new CraftingManager(allAvailableRecipes, recipeUIPrefab, recipeUIParent)
        );
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
