using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Settings", menuName = "Settings/Inventory Settings", order = 0)]
public class GridSettings : ScriptableObject
{
    [SerializeField] private Vector2Int tileSize;
    [SerializeField] private float tileDistance;

    public Vector2Int TileSize => tileSize;
    public float TileDistance => tileDistance;
}
