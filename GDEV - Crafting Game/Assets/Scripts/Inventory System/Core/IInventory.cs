public interface IInventory : IService
{
    /// <summary>
    /// Add a certain amount of items to the inventory of the given type.
    /// </summary>
    public void Add(params ItemStack[] itemStacks);

    /// <summary>
    /// Remove a certain amount of items from the inventory of the given type.
    /// </summary>
    public void Remove(params ItemStack[] itemStacks);

    /// <returns>True if the given type is present, in the given amount. False if not.</returns>
    public bool Contains(params ItemStack[] itemStacks);
}
