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
    
    [Header("Inventory System")]
    [SerializeField] private RectTransform inventoryRoot;
    
    [Header("References")]
    [SerializeField] private GameObject endScreen;
    [SerializeField] private ItemData winningItem;

    private ServiceLocator serviceLocator = null;
    private TickManager tickManager = null;

    private InteractionManager interactionManager =  null;
    private InventoryManager inventoryManager = null;
    private GatherManager gatherManager = null;
    private CraftingManager craftingManager = null;

    private bool isRunning = false;

    private void Awake()
    {
        // Order is important : First ServiceLocator, Second TickManager and Third InteractionManager
        serviceLocator = new ServiceLocator();
        
        tickManager = new TickManager();
        serviceLocator.Add(tickManager, typeof(ITickManager));

        interactionManager = new InteractionManager();
        serviceLocator.Add(interactionManager, typeof(IInteractionManager));
        tickManager.Add(interactionManager);

        inventoryManager = new InventoryManager(inventoryRoot);
        serviceLocator.Add(inventoryManager.Inventory, typeof(IInventory));

        gatherManager = new GatherManager(baseGatherComponent);
        serviceLocator.Add(gatherManager, typeof(IGatherManager));
        tickManager.Add(gatherManager);

        craftingManager = new CraftingManager(recipeUIParent, craftingQueueUIParent, craftingSystemToolTip, craftingQueueProgressBar);
        tickManager.Add(craftingManager);
    }

    private void Update()
    {
        if (!isRunning) { return; }
        tickManager.OnUpdate();
    }

    private void FixedUpdate()
    {
        if (!isRunning) { return; }
        tickManager.OnFixedUpdate();

        if (inventoryManager.Inventory.Contains(new ItemStack(winningItem, 1)))
        {
            isRunning = false;
            endScreen.SetActive(true);
        }
    }

    public void StartGame(GameObject startScreen)
    {
        startScreen.SetActive(false);
        isRunning = true;
        tickManager.OnStart();
    }
}
