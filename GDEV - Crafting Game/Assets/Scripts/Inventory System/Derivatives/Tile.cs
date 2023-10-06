using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Tile
{
    public event Action<ItemStack> OnAltered = null;

    private ItemStack contents = default;

    public ItemStack Contents => contents;
    public bool HasContents => contents.Amount > 0;

    #region Modifiers
    public void SetContents(ItemStack _stack)
    {
        contents = _stack;
        OnAltered?.Invoke(contents);
    }

    public void SetContents(ItemData _type, int _amount)
    {
        contents = new ItemStack(_type, _amount);
        OnAltered?.Invoke(contents);
    }

    public void Clear()
    {
        contents = default;
        OnAltered?.Invoke(contents);
    }

    public void ChangeValue(int _amount)
    {
        contents.Amount += _amount;
        if (contents.Amount <= 0) 
        { 
            Clear(); 
            return; 
        }
        OnAltered?.Invoke(contents);
    }
    #endregion

    #region Checks
    public bool CompareType(ItemData type) => contents.Item == type;

    public bool CompareType(Tile other) => contents.Item == other.contents.Item;

    public bool Contains(int amount) => contents.Amount >= amount;
    #endregion
}
