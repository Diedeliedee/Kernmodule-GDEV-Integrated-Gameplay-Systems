using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class InventoryManager
{
    private readonly RectTransform root = null;
    private readonly GridSettings settings = null;
    private readonly GridInventory inventory = null;

    private SlotElement[,] elementGrid = null;

    public IInventory Inventory => inventory;

    public InventoryManager(RectTransform _gridRoot)
    {
        //  Gather required information.
        root = _gridRoot;
        settings = Resources.Load<GridSettings>("Settings/Grid");

        //  Initiate back-end.
        inventory = new GridInventory(settings.Resolution.x, settings.Resolution.y);

        //  Initiate front-end.
        elementGrid = InstantiateGrid(settings.Resolution, root.sizeDelta, inventory.Items);
    }

    private SlotElement[,] InstantiateGrid(Vector2Int _gridRes, Vector2 _gridSize, Tile[,] tiles)
    {
        var slotSize = new Vector2(_gridSize.x / _gridRes.x, _gridSize.y / _gridRes.y);
        var grid = new SlotElement[_gridRes.x, _gridRes.y];

        for (int x = 0; x < settings.Resolution.x; x++)
        {
            for (int y = 0; y < settings.Resolution.y; y++)
            {
                var createdSlot = GameObject.Instantiate(Resources.Load<GameObject>("Grid Inventory/PRE_Slot"), root);
                var transform = createdSlot.GetComponent<RectTransform>();

                createdSlot.name = $"Grid Slot ({x}, {y})";
                transform.sizeDelta = slotSize;
                transform.anchoredPosition = new Vector2(x / settings.Resolution.x * root.sizeDelta.x, y / settings.Resolution.y * root.sizeDelta.y);

                grid[x, y] = new SlotElement(transform, tiles[x, y]);
            }
        }
        return grid;
    }
}
