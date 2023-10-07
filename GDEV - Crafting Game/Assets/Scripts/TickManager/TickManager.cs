using System.Collections.Generic;

public class TickManager : ITickManager
{
    private readonly List<IUpdatable> subscribedObjects;

    public TickManager()
    {
        subscribedObjects = new List<IUpdatable>();
    }

    public void Add(IUpdatable _updatable)
    {
        if (!subscribedObjects.Contains(_updatable)) 
        {
            subscribedObjects.Add(_updatable);
        }
    }

    public void Remove(IUpdatable _updatable)
    {
        if (subscribedObjects.Contains(_updatable))
        {
            subscribedObjects.Remove(_updatable);
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
