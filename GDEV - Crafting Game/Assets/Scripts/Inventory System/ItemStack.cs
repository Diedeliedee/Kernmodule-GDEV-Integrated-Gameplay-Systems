public struct ItemStack
{
    public readonly ItemData Item;
    public readonly int Amount;

    public ItemStack(ItemData _item, int _amount)
    {
        Item = _item;
        Amount = _amount;
    }
}
