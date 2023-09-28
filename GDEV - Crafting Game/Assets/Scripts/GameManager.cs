using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private readonly ServiceLocator serviceLocator = new();
    private TickManager tickManager;

    private void Awake()
    {
        tickManager = new TickManager();
        serviceLocator.Add(tickManager);

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
