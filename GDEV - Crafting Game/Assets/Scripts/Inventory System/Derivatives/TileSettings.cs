
using UnityEngine;

 public class TileSettings : ScriptableObject
{
    [SerializeField] private float width, height;
    [SerializeField] private float distance;

    public float Width => width;
    public float Height => height;
    public float Distance => distance;

}
