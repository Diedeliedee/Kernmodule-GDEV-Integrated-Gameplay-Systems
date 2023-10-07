using UnityEngine;

public class GridInventory : IInventory
{
    public Tile[,] Items { get; private set; }

    public GridInventory(int _width, int _height)
    {
        Items = new Tile[_width, _height];

        bool OnIterate(int x, int y)
        {
            Items[x, y] = new Tile();
            return false;
        }

        Loop(OnIterate);
    }

    public void Add(params ItemStack[] itemStacks)
    {
        //  Iterate through every item stack.
        foreach (var itemStack in itemStacks)
        {
            //  Iterate through every slot.
            bool OnIterate(int x, int y)
            {
                //  If the grid in the slot is occupied..
                if (Items[x, y].HasContents)
                {
                    //  Keep looping if it isn't the same type as the passed in item stack.
                    if (!Items[x, y].CompareType(itemStack.Item)) return false;

                    //  Otherwise, add to the value of the stack.
                    Items[x, y].ChangeValue(itemStack.Amount);
                    return true;
                }

                //  Otherwise, set the empty slot to be the passed in item stack.
                else
                {
                    Items[x, y].SetContents(itemStack);
                    return true;
                }
            }

            Loop(OnIterate);
        }
    }

    public bool Contains(params ItemStack[] itemStacks)
    {
        var checksPassed = 0;

        //  Iterate through every item stack.
        foreach (var itemStack in itemStacks)
        {
            //  Iterate through every slot.
            bool OnIterate(int x, int y)
            {
                if (!Items[x, y].Contains(itemStack.Amount)) return false;  //  Keep looping if the slot has an insufficient amount.
                if (!Items[x, y].CompareType(itemStack.Item)) return false; //  Keep looping if the data of the stack is not the same.

                //  If the sufficient items of the type have been found, stop the loop, and confirm the check.
                checksPassed++;
                return true;
            }

            Loop(OnIterate);
        }

        //  Return true if the checks passed correspond with the amount of items to check.
        return checksPassed == itemStacks.Length;
    }

    public void Remove(params ItemStack[] itemStacks)
    {
        //  Iterate through every item stack.
        foreach (var itemStack in itemStacks)
        {
            //  Iterate through every slot.
            bool OnIterate(int x, int y)
            {
                //  Keep looping if the data of the stack is not the same.
                if (!Items[x, y].CompareType(itemStack.Item)) return false;

                //  If the item on the slot is of the same type than of the passed in stack, remove the amount.
                Items[x, y].ChangeValue(-itemStack.Amount);
                return true;
            }

            Loop(OnIterate);
        }
    }

    /// <summary>
    /// Add an item stack to the inventory at a specific slot.
    /// </summary>
    public void SetAt(Vector2Int coordinates, ItemStack stack)
    {
        Items[coordinates.x, coordinates.y].SetContents(stack);
    }

    /// <summary>
    /// Add an item stack to the inventory at a specific slot, and re-add whatever contents the stack may replace.
    /// </summary>
    public void ReplaceAt(Vector2Int coordinates, ItemStack stack)
    {
        var selectedTile = Items[coordinates.x, coordinates.y];

        //  If the tile is empty, just set the contents.
        if (!selectedTile.HasContents)
        {
            selectedTile.SetContents(stack);
            return;
        }

        //  If the tile is occupied, cache the contents, and re-add them to the inventory.
        var cachedContents = selectedTile.Contents;

        selectedTile.SetContents(stack);
        Add(cachedContents);
    }

    /// <summary>
    /// Loops through the entire inventory, and call the 'onIterate' predicate every iteration.
    /// </summary>
    private void Loop(Iteration onIterate, int xStart = 0, int yStart = 0)
    {
        for (int x = yStart; x < Items.GetLength(0); x++)
        {
            for (int y = xStart; y < Items.GetLength(1); y++)
            {
                if (onIterate(x, y)) return;
            }
        }
    }

    private delegate bool Iteration(int x, int y);
}
