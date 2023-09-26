using UnityEngine;

public class ItemData : ScriptableObject
{
    [SerializeField] private Sprite image;

    public Sprite Image => image;
}
