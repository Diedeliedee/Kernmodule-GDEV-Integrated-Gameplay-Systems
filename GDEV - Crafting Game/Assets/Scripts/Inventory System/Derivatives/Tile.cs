using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Tile
{
    //  Public:
    public readonly RectTransform Transform = null;

    //  Private:
    private ItemStack contents = default;

    //  Properties:
    public ItemStack Contents => contents;
    public bool HasContents => contents.Amount > 0;

    public Tile(int _xCoordinate, int _yCoordinate, Transform _parent = null)
    {
        //var createdTile = new GameObject($"Inventory Tile ({_xCoordinate}, {_yCoordinate})");

        //Transform = createdTile.AddComponent<RectTransform>();
        //Transform.parent = _parent;
    }

    #region Modifiers
    public void SetContents(ItemStack _stack) => contents = _stack;

    public void SetContents(ItemData _type, int _amount) => contents = new ItemStack(_type, _amount);

    public void Empty() => contents = default;

    public void ChangeValue(int _amount)
    {
        contents.Amount += _amount;

        if (contents.Amount <= 0) { Empty(); }
    }
    #endregion

    #region Checks
    public bool CompareType(ItemData type) => contents.Item == type;
    public bool CompareType(Tile other) => contents.Item == other.contents.Item;

    public bool Contains(int amount) => contents.Amount >= amount;
    #endregion
}
