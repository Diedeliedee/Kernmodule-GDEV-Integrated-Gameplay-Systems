using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] private Sprite image;

    public Sprite Image => image;
}
