using System.Collections.Generic;

public class GatherInfo
{
    public Dictionary<ItemData, GatherChance> gatherItems;

    public GatherInfo()
    {
        gatherItems = new Dictionary<ItemData, GatherChance>();
    }

    public void Decorate(GatherComponent gatherComponent)
    {
        foreach (var currentGatherItem in gatherComponent.gatherItems)
        {
            if (gatherItems.ContainsKey(currentGatherItem.key))
            {
                GatherChance gatherChance = gatherItems[currentGatherItem.key];
                gatherChance.gatherChancePercentage += currentGatherItem.value.gatherChancePercentage;
                gatherItems[currentGatherItem.key] = gatherChance;
            }
            else
            {
                gatherItems.Add(currentGatherItem.key, currentGatherItem.value);
            }
        }
    }
}

