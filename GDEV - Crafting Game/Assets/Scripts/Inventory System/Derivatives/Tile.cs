using System;

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
    public bool CompareType(ItemData _type) => contents.Item == _type;

    public bool CompareType(Tile _other) => contents.Item == _other.contents.Item;

    public bool Contains(int _amount) => contents.Amount >= _amount;
    #endregion
}
