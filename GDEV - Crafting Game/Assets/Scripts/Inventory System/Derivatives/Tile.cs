using UnityEngine;

public class Tile
{
    private ItemStack contents = default;

    public ItemStack Contents => contents;
    public bool HasContents => contents.Amount > 0;

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
