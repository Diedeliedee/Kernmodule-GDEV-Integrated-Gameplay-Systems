﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GridInventory : IInventory
{
    private readonly RectTransform transform = null;
    private readonly ItemStack[,] itemGrid = null;

    public GridInventory(int _width, int _height, Transform _parent = null)
    {
        transform = new GameObject("Grid Inventory", typeof(RectTransform)).GetComponent<RectTransform>();
        itemGrid = new ItemStack[_width, _height];
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
                if (itemGrid[x, y].HasValue)
                {
                    //  Keep looping if it isn't the same type as the passed in item stack.
                    if (!itemGrid[x, y].CompareData(itemStack.Item)) return false;

                    //  Otherwise, add to the value of the stack.
                    itemGrid[x, y].Amount += itemStack.Amount;
                    return true;
                }

                //  Otherwise, set the empty slot to be the passed in item stack.
                else
                {
                    itemGrid[x, y] = itemStack;
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
                if (!itemGrid[x, y].Contains(itemStack.Amount)) return false;   //  Keep looping if the slot has an insufficient amount.
                if (!itemGrid[x, y].CompareData(itemStack.Item)) return false;  //  Keep looping if the data of the stack is not the same.

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
                if (!itemGrid[x, y].CompareData(itemStack.Item)) return false;

                //  If the item on the slot is of the same type than of the passed in stack, remove the amount.
                itemGrid[x, y].Amount -= itemStack.Amount;
                if (itemGrid[x, y].Amount <= 0) { itemGrid[x, y] = ItemStack.Empty; }
                return true;
            }

            Loop(OnIterate);
        }
    }

    private void Loop(Iteration onIterate, int xStart = 0, int yStart = 0)
    {
        for (int y = yStart; y < itemGrid.GetLength(0); y++)
        {
            for (int x = xStart; x < itemGrid.GetLength(1); x++)
            {
                if (onIterate(x, y)) return;
            }
        }
    }

    private delegate bool Iteration(int x, int y);
}
