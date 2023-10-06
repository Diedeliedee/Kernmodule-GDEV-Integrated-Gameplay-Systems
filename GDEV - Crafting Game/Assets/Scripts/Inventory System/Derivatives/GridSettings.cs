using UnityEngine;

[CreateAssetMenu(fileName = "New Grid Settings", menuName = "Settings/Grid Settings", order = 1)]
public class GridSettings : ScriptableObject
{
    [SerializeField] private Vector2Int resolution;

    public Vector2Int Resolution => resolution;
}
