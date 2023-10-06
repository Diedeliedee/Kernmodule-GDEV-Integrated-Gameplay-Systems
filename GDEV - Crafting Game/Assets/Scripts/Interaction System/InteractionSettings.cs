using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "New Interaction Settings", menuName = "Settings/Interaction Settings", order = 1)]
public class InteractionSettings : ScriptableObject
{
    [SerializeField] private LayerMask interactionMask;

    public LayerMask InteractionMask => interactionMask;
}
