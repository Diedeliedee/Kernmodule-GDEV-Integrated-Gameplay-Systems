using UnityEngine;

[System.Serializable]
public struct GatherChance
{
    [Range(0, 100)] public float gatherChancePercentage;
    [Range(0, 100)] public int maxStackSize;
}

