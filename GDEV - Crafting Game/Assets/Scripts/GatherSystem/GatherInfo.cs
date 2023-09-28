using System.Collections.Generic;

public class GatherInfo
{
    public Dictionary<ItemData, GatherChance> gatherItems;

    public GatherInfo(Dictionary<ItemData, GatherChance> _gatherItems)
    {
        gatherItems = _gatherItems;
    }

    public struct GatherChance
    {
        public float gatherChancePercentage;
        public int maxStackSize;
    }
}

