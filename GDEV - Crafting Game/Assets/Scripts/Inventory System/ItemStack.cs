public struct ItemStack
{
    public readonly ItemData Item;
    public int Amount;

    public bool HasValue => Item != null && Amount > 0;

    public static ItemStack Empty => new ItemStack(null, 0);

    public ItemStack(ItemData _item, int _amount)
    {
        Item = _item;
        Amount = _amount;
    }

    public bool CompareData(ItemData obj)
    {
        return Item == obj;
    }

    public bool Contains(int amount)
    {
        return Amount >= amount;
    }
}
