using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform

    private readonly ServiceLocator serviceLocator = new();
    private GridInventory inventory;
    private TickManager tickManager;

    private void Awake()
    {
        tickManager = new TickManager();
        inventory = new GridInventory(10, 10, transform);

        serviceLocator.Add(tickManager);
        serviceLocator.Add(inventory);

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
