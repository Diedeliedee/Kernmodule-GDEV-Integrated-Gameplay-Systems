using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private RectTransform canvas;

    private readonly ServiceLocator serviceLocator = new();
    private InventoryManager inventoryManager = null;
    private TickManager tickManager = null;

    private void Awake()
    {
        tickManager = new TickManager();
        inventoryManager = new InventoryManager(canvas);

        serviceLocator.Add(tickManager);
        serviceLocator.Add(inventoryManager.Inventory, typeof(IInventory));

        tickManager.Add(new GatherSystem(
            new Dictionary<ItemData, GatherInfo.GatherChance>()
            {
                // Insert Standard ItemData
            }
        ));
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
