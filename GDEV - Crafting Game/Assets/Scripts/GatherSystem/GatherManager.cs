using System.Collections.Generic;
using UnityEngine;

public class GatherManager : IUpdatable
{
    private GatherInfo gatherInfo;
    private IInventory inventory;

    public GatherManager(Dictionary<ItemData, GatherInfo.GatherChance> startingSettings) 
    {
        gatherInfo = new GatherInfo(startingSettings);
    }

    public void OnStart()
    {
        inventory = ServiceLocator.Instance.Get<IInventory>();
    }

    public void OnFixedUpdate()
    {
        List<ItemStack> itemStacks = new();

        foreach (KeyValuePair<ItemData, GatherInfo.GatherChance> gatherItem in gatherInfo.gatherItems)
        {
            float randomPercentage = Random.Range(0.00001f, 100.0f);

            if (randomPercentage < gatherItem.Value.gatherChancePercentage)
            {
                int randomStackSize = Random.Range(0, gatherItem.Value.maxStackSize + 1);
                itemStacks.Add(new(gatherItem.Key, randomStackSize));
            }
        }

        inventory.Add(itemStacks.ToArray());
    }

    public void OnUpdate() { }
}
