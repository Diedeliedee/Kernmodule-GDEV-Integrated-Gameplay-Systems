public interface ITickManager : IService
{
    /// <summary>
    /// Add an IUpdatable object to the list of objects updated by the TickManager
    /// </summary>
    void Add(IUpdatable updatable);

    /// <summary>
    /// Remove an IUpdatable object to the list of objects updated by the TickManager
    /// </summary>
    void Remove(IUpdatable updatable);
}

