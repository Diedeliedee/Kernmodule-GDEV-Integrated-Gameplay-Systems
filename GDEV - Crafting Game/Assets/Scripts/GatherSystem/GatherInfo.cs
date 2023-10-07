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
            if (gatherItems.ContainsKey(currentGatherItem.Key))
            {
                GatherChance gatherChance = gatherItems[currentGatherItem.Key];
                gatherChance.gatherChancePercentage += currentGatherItem.Value.gatherChancePercentage;
                gatherItems[currentGatherItem.Key] = gatherChance;
            }
            else
            {
                gatherItems.Add(currentGatherItem.Key, currentGatherItem.Value);
            }
        }
    }
}
