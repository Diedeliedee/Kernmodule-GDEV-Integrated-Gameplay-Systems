using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : IUpdatable
{
    //  Core:
    private readonly GridSettings settings = null;
    private readonly GridInventory inventory = null;

    //  UI:
    private readonly RectTransform root = null;
    private readonly GridLayoutGroup gridLayout = null;
    private SlotElement[,] elementGrid = null;

    //  Properties:
    public IInventory Inventory => inventory;

    public InventoryManager(RectTransform _gridRoot)
    {
        //  Gather required information.
        settings = Resources.Load<GridSettings>("Settings/Grid");
        root = _gridRoot;
        gridLayout = root.GetComponent<GridLayoutGroup>();

        //  Initiate back-end.
        inventory = new GridInventory(settings.Resolution.x, settings.Resolution.y);

        //  Initiate front-end.
        elementGrid = InstantiateGrid(inventory.Items);
    }

    public void OnUpdate()
    {
        for (int x = 0; x < elementGrid.GetLength(0); x++)
        {
            for (int y = 0; y < elementGrid.GetLength(1); y++)
            {
                elementGrid[x, y].Tick(Time.deltaTime);
            }
        }
    }

    public void OnStart() { }

    public void OnFixedUpdate() { }

    private SlotElement[,] InstantiateGrid(Tile[,] tiles)
    {
        var grid = new SlotElement[settings.Resolution.x, settings.Resolution.y];
        var slotSize = GetDesiredSlotSize();

        gridLayout.cellSize = new Vector2(slotSize.x, slotSize.x);
        gridLayout.spacing = new Vector2(settings.Spacing, settings.Spacing);
        gridLayout.constraintCount = settings.Resolution.x;

        //  Loop two dimensionally.
        for (int x = 0; x < settings.Resolution.x; x++)
        {
            for (int y = 0; y < settings.Resolution.y; y++)
            {
                var createdSlot = Object.Instantiate(Resources.Load<GameObject>("Grid Inventory/PRE_Slot"), root);
                var transform = createdSlot.GetComponent<RectTransform>();

                createdSlot.name = $"Grid Slot ({x}, {y})";
                grid[x, y] = new SlotElement(transform, tiles[x, y], settings);
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
}
