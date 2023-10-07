using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Gather System")]
    [SerializeField] private GatherComponent baseGatherComponent;

    [Header("Crafting System")]
    [SerializeField] private RectTransform recipeUIParent;
    [SerializeField] private RectTransform craftingQueueUIParent;
    [SerializeField] private RectTransform craftingSystemToolTip;
    [SerializeField] private Image craftingQueueProgressBar;
    
    [Header("Reference")]
    [SerializeField] private RectTransform inventoryRoot;

    private ServiceLocator serviceLocator = null;
    private TickManager tickManager = null;

    private InteractionManager interactionManager =  null;
    private InventoryManager inventoryManager = null;
    private GatherManager gatherManager = null;
    private CraftingManager craftingManager = null;

    private void Awake()
    {
        // Order is important : First ServiceLocator, Second TickManager and Third InteractionManager
        serviceLocator = new ServiceLocator();
        
        tickManager = new TickManager();
        serviceLocator.Add(tickManager, typeof(ITickManager));

        interactionManager = new InteractionManager();
        serviceLocator.Add(interactionManager, typeof(InteractionManager));
        tickManager.Add(interactionManager);

        inventoryManager = new InventoryManager(inventoryRoot);
        serviceLocator.Add(inventoryManager.Inventory, typeof(IInventory));

        gatherManager = new GatherManager(baseGatherComponent);
        serviceLocator.Add(gatherManager, typeof(IGatherManager));
        tickManager.Add(gatherManager);

        craftingManager = new CraftingManager(recipeUIParent, craftingQueueUIParent, craftingSystemToolTip, craftingQueueProgressBar);
        tickManager.Add(craftingManager);
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
