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
        elementGrid = InstantiateGrid(inventory.Items);
    }

    private SlotElement[,] InstantiateGrid(Tile[,] tiles)
    {
        var grid = new SlotElement[settings.Resolution.x, settings.Resolution.y];
        var slotSize = GetDesiredSlotSize();

        //  Loop two dimensionally.
        for (int x = 0; x < settings.Resolution.x; x++)
        {
            for (int y = 0; y < settings.Resolution.y; y++)
            {
                //  Cache created objects.
                var createdSlot = Object.Instantiate(Resources.Load<GameObject>("Grid Inventory/PRE_Slot"), root);
                var transform = createdSlot.GetComponent<RectTransform>();
                var position = GetDesiredSlotPosition(x, y, slotSize);

                //  Alter transform of the slot.
                createdSlot.name = $"Grid Slot ({x}, {y})";
                transform.sizeDelta = slotSize;
                transform.anchoredPosition = position;

                //  Create slot elements.
                grid[x, y] = new SlotElement(transform, tiles[x, y]);
            }
        }
        return grid;
    }

    private Vector2 GetDesiredSlotSize()
    {
        var size = new Vector2();

        //  Divide the width of the grid size by chunks depending on the resolution.
        size.x = root.sizeDelta.x / settings.Resolution.x;  
        size.y = root.sizeDelta.y / settings.Resolution.y;

        //  Apply spacing to the width and height of the slot size.
        size.x -= settings.Spacing;         
        size.y -= settings.Spacing;

        return size;
    }

    private Vector2 GetDesiredSlotPosition(int x, int y, Vector2 _slotSize)
    {
        var position = new Vector2();

        //  Create percentage [0...1] value.
        position.x = (float)x / settings.Resolution.x;   
        position.y = (float)-y / settings.Resolution.y;

        //  Multiply the percantage by the width and height of the parent transform. 
        position.x *= root.sizeDelta.x;                             
        position.y *= root.sizeDelta.y;

        //  Presuming the pivot of the slot is in the middle, offset by half of it's size.
        position.x += _slotSize.x;                                  
        position.y += _slotSize.y;

        return position;
    }
}
