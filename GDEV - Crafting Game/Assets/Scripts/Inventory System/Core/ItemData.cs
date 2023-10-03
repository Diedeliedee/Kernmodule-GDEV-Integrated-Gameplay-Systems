using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite image;

    public Sprite Image => image;
    public string ItemName => itemName;
}
