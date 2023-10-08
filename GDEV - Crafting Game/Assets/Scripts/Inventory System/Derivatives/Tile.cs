using System;

public class Tile
{
    public event Action<ItemStack> OnAltered = null;

    public event Action<int> OnValueChanged = null;
    public event Action<ItemData> OnTypeChanged = null;

    private ItemStack contents = default;

    public ItemStack Contents => contents;
    public bool HasContents => contents.Amount > 0;
    #region Modifiers
    public void SetContents(ItemStack _stack)
    {
        OnAltered?.Invoke(_stack);
        if (contents.Item != _stack.Item) OnTypeChanged?.Invoke(_stack.Item);
        if (contents.Amount != _stack.Amount) OnValueChanged?.Invoke(_stack.Amount);
        contents = _stack;
    }

    public void SetContents(ItemData _type, int _amount)
    {
        var newStack = new ItemStack(_type, _amount);

        SetContents(newStack);
    }

    public void Clear()
    {
        SetContents(default);
    }

    public void ChangeValue(int _amount)
    {
        if (_amount == 0) return;

        contents.Amount += _amount;
        if (contents.Amount <= 0) 
        { 
            Clear(); 
            return; 
        }
        OnAltered?.Invoke(contents);
        OnValueChanged?.Invoke(contents.Amount);
    }
    #endregion

    #region Checks
    public bool CompareType(ItemData _type) => contents.Item == _type;

    public bool CompareType(Tile _other) => contents.Item == _other.contents.Item;

    public bool Contains(int _amount) => contents.Amount >= _amount;
    #endregion
}
