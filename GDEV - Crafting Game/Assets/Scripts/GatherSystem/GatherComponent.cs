using UnityEngine;

[CreateAssetMenu(fileName = "New GatherComponent", menuName = "GatherComponent")]
public class GatherComponent : ScriptableObject
{
    public SerializableDictionary<ItemData, GatherChance> gatherItems;
}
