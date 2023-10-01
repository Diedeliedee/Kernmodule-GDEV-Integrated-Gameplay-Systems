using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class InventoryManager
{
    private RectTransform transform = null;
    private GridInventory inventory = null;

    private GridSettings settings = null;

    public IInventory Inventory => inventory;

    public InventoryManager(Transform _gridParent)
    {
        var createdInventoryObject = new GameObject("Grid Inventory");

        transform = createdInventoryObject.AddComponent<RectTransform>();
        transform.parent = _gridParent;

        inventory = new GridInventory(10, 10);

        settings = Resources.Load<GridSettings>("Grid Inventory/Grid Settings");
    }
}
