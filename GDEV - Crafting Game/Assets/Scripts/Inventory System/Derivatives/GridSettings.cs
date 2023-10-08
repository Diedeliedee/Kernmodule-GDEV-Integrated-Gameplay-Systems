using UnityEngine;

[CreateAssetMenu(fileName = "New Grid Settings", menuName = "Settings/Grid Settings", order = 1)]
public class GridSettings : ScriptableObject
{
    [SerializeField] private Vector2Int resolution;
    [SerializeField] private float spacing;
    [Space]
    [SerializeField] private float updateSizeMultiplier;
    [SerializeField] private float updateLerpSpeed;

    public Vector2Int Resolution => resolution;
    public float Spacing => spacing;

    public float PopSizeMultiplier => updateSizeMultiplier;
    public float PopLerpSpeed => updateLerpSpeed;
}
