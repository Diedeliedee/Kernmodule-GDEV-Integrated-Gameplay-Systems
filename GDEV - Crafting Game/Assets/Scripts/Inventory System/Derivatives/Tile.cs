using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Tile
{
    private readonly RectTransform transform = null;

    public Tile(int _xCoordinate, int _yCoordinate, Transform _parent = null)
    {
        transform = Resources.Load<RectTransform>("Grid Inventory/PRE_Tile");

    }
}
