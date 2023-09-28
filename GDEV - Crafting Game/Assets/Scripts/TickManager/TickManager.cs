using System.Collections.Generic;

public class TickManager : ITickManager
{
    private readonly List<IUpdatable> subscribedObjects = new();

    public void Add(IUpdatable updatable)
    {
        if (!subscribedObjects.Contains(updatable)) 
        {
            subscribedObjects.Add(updatable);
        }
    }

    public void Remove(IUpdatable updatable)
    {
        if (subscribedObjects.Contains(updatable))
        {
            subscribedObjects.Remove(updatable);
        }
    }

    public void OnStart()
    {
        foreach (IUpdatable updatable in subscribedObjects)
        {
            updatable.OnStart();
        }
    }

    public void OnUpdate()
    {
        foreach (IUpdatable updatable in subscribedObjects)
        {
            updatable.OnUpdate();
        }
    }

    public void OnFixedUpdate()
    {
        foreach (IUpdatable updatable in subscribedObjects)
        {
            updatable.OnFixedUpdate();
        }
    }
}
