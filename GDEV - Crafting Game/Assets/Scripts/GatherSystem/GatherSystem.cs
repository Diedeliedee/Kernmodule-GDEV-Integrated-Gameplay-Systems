using System.Collections.Generic;
using UnityEngine;

public class GatherSystem : IUpdatable
{
    private GatherInfo gatherInfo;

    public GatherSystem(Dictionary<ItemData, GatherInfo.GatherChance> startingSettings) 
    {
        gatherInfo = new GatherInfo(startingSettings);
    }

    public void OnStart() { }

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

        ServiceLocator.Instance.Get<IInventory>().Add(itemStacks.ToArray());
    }

    public void OnUpdate() { }
}
