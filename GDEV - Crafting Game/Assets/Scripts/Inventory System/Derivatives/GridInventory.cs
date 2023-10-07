using UnityEngine;

public class GridInventory : IInventory
{
    public Tile[,] Items { get; private set; }

    public GridInventory(int _width, int _height)
    {
        Items = new Tile[_width, _height];

        bool OnIterate(int _x, int _y)
        {
            Items[_x, _y] = new Tile();
            return false;
        }

        Loop(OnIterate);
    }

    public void Add(params ItemStack[] _itemStacks)
    {
        //  Iterate through every item stack.
        foreach (var itemStack in _itemStacks)
        {
            //  Iterate through every slot.
            bool OnIterate(int _x, int _y)
            {
                //  If the grid in the slot is occupied..
                if (Items[_x, _y].HasContents)
                {
                    //  Keep looping if it isn't the same type as the passed in item stack.
                    if (!Items[_x, _y].CompareType(itemStack.Item)) return false;

                    //  Otherwise, add to the value of the stack.
                    Items[_x, _y].ChangeValue(itemStack.Amount);
                    return true;
                }

                //  Otherwise, set the empty slot to be the passed in item stack.
                else
                {
                    Items[_x, _y].SetContents(itemStack);
                    return true;
                }
            }

            Loop(OnIterate);
        }
    }

    public bool Contains(params ItemStack[] _itemStacks)
    {
        var checksPassed = 0;

        //  Iterate through every item stack.
        foreach (var itemStack in _itemStacks)
        {
            //  Iterate through every slot.
            bool OnIterate(int _x, int _y)
            {
                if (!Items[_x, _y].Contains(itemStack.Amount)) return false;  //  Keep looping if the slot has an insufficient amount.
                if (!Items[_x, _y].CompareType(itemStack.Item)) return false; //  Keep looping if the data of the stack is not the same.

                //  If the sufficient items of the type have been found, stop the loop, and confirm the check.
                checksPassed++;
                return true;
            }

            Loop(OnIterate);
        }

        //  Return true if the checks passed correspond with the amount of items to check.
        return checksPassed == _itemStacks.Length;
    }

    public void Remove(params ItemStack[] _itemStacks)
    {
        //  Iterate through every item stack.
        foreach (var itemStack in _itemStacks)
        {
            //  Iterate through every slot.
            bool OnIterate(int _x, int _y)
            {
                //  Keep looping if the data of the stack is not the same.
                if (!Items[_x, _y].CompareType(itemStack.Item)) return false;

                //  If the item on the slot is of the same type than of the passed in stack, remove the amount.
                Items[_x, _y].ChangeValue(-itemStack.Amount);
                return true;
            }

            Loop(OnIterate);
        }
    }

    /// <summary>
    /// Add an item stack to the inventory at a specific slot.
    /// </summary>
    public void SetAt(Vector2Int _coordinates, ItemStack _stack)
    {
        Items[_coordinates.x, _coordinates.y].SetContents(_stack);
    }

    /// <summary>
    /// Add an item stack to the inventory at a specific slot, and re-add whatever contents the stack may replace.
    /// </summary>
    public void ReplaceAt(Vector2Int _coordinates, ItemStack _stack)
    {
        var selectedTile = Items[_coordinates.x, _coordinates.y];

        //  If the tile is empty, just set the contents.
        if (!selectedTile.HasContents)
        {
            selectedTile.SetContents(_stack);
            return;
        }

        //  If the tile is occupied, cache the contents, and re-add them to the inventory.
        var cachedContents = selectedTile.Contents;

        selectedTile.SetContents(_stack);
        Add(cachedContents);
    }

    /// <summary>
    /// Loops through the entire inventory, and call the 'onIterate' predicate every iteration.
    /// </summary>
    private void Loop(Iteration _onIterate, int _xStart = 0, int _yStart = 0)
    {
        for (int x = _yStart; x < Items.GetLength(0); x++)
        {
            for (int y = _xStart; y < Items.GetLength(1); y++)
            {
                if (_onIterate(x, y)) return;
            }
        }
    }

    private delegate bool Iteration(int _x, int _y);
}
