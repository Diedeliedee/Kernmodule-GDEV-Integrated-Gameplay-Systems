using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class InteractionSettings : ScriptableObject
{
    [SerializeField] private LayerMask interactionMask;

    public LayerMask InteractionMask => interactionMask;
}
