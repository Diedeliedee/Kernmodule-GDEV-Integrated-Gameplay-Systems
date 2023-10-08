using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

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
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private GameObject inventoryUIObject;
    [SerializeField] private GameObject craftingUIObject;
    [SerializeField] private ItemData winningItem;
    [SerializeField] private VisualEffect winEffect;

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

        interactionManager = new InteractionManager(canvas);
        serviceLocator.Add(interactionManager, typeof(IInteractionManager));
        tickManager.Add(interactionManager);

        inventoryManager = new InventoryManager(inventoryRoot);
        serviceLocator.Add(inventoryManager.Inventory, typeof(IInventory));
        tickManager.Add(inventoryManager);

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

        if (Input.GetKeyDown(KeyCode.W))
        {
            inventoryManager.Inventory.Add(new ItemStack(winningItem, 1));
        }
    }

    private void FixedUpdate()
    {
        if (!isRunning) { return; }
        tickManager.OnFixedUpdate();

        if (inventoryManager.Inventory.Contains(new ItemStack(winningItem, 1)))
        {
            isRunning = false;
            endScreen.SetActive(true);
            craftingUIObject.SetActive(false);
            inventoryUIObject.SetActive(false);
            winEffect.Play();
        }
    }

    public void StartGame(GameObject startScreen)
    {
        startScreen.SetActive(false);
        isRunning = true;
        tickManager.OnStart();
    }
}
