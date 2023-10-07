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
    private InteractionManager interactionManager =  null;
    private InventoryManager inventoryManager = null;
    private TickManager tickManager = null;
    

    private void Awake()
    {
        serviceLocator = new ServiceLocator();
        interactionManager = new InteractionManager();
        tickManager = new TickManager();
        inventoryManager = new InventoryManager(inventoryRoot);

        GatherManager gatherManager = new(baseGatherComponent);

        serviceLocator.Add(tickManager, typeof(ITickManager));
        serviceLocator.Add(inventoryManager.Inventory, typeof(IInventory));
        serviceLocator.Add(gatherManager, typeof(IGatherManager));

        tickManager.Add(gatherManager);
        tickManager.Add(new CraftingManager(recipeUIParent, craftingQueueUIParent, craftingQueueProgressBar));
        tickManager.Add(interactionManager);
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
