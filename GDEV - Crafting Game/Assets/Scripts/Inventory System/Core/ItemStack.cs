public struct ItemStack
{
    public ItemData Item;
    public int Amount;

    public ItemStack(ItemData _item, int _amount)
    {
        Item = _item;
        Amount = _amount;
    }
}
