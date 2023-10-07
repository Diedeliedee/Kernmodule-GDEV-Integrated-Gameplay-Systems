using UnityEngine;

[CreateAssetMenu(fileName = "New Interaction Settings", menuName = "Settings/Interaction Settings", order = 1)]
public class InteractionSettings : ScriptableObject
{
    [SerializeField] private LayerMask interactionMask;

    public LayerMask InteractionMask => interactionMask;
}
