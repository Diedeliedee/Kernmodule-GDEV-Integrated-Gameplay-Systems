using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "New Grid Settings", menuName = "Settings/Grid Settings", order = 0)]
public class GridSettings : ScriptableObject
{
    [SerializeField] private Vector2Int resolution;

    public Vector2Int Resolution => resolution;
}
