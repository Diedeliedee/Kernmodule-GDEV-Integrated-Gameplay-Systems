using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Gather System")]
    [SerializeField] private GatherComponent baseGatherComponent;

    [Header("Crafting System")]
    [SerializeField] private RectTransform recipeUIParent;
    [SerializeField] private RectTransform craftingQueueUIParent;
    [SerializeField] private Image craftingQueueProgressBar;
    
    [Header("Reference")]
    [SerializeField] private RectTransform inventoryRoot;

    private ServiceLocator serviceLocator = null;
    private InventoryManager inventoryManager = null;
    private TickManager tickManager = null;

    private void Awake()
    {
        serviceLocator = new ServiceLocator();

        tickManager = new TickManager();
        inventoryManager = new InventoryManager(inventoryRoot);

        serviceLocator.Add(tickManager, typeof(ITickManager));
        serviceLocator.Add(inventoryManager.Inventory, typeof(IInventory));

        tickManager.Add(new GatherManager(baseGatherComponent));
        tickManager.Add(new CraftingManager(recipeUIParent, craftingQueueUIParent, craftingQueueProgressBar));
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
