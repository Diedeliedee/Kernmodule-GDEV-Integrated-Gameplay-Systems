using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator
{
    //  Service Item Pool:
    private readonly Dictionary<AvailableService, IService> services = new Dictionary<AvailableService, IService>();

    //  Getter:
    public static ServiceLocator Instance { get; private set; }

    public ServiceLocator()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Other instance of a service locator already exists.");
            return;
        }
        Instance = this;
    }

    public void Add(AvailableService key, IService service)
    {
        if (services.ContainsKey(key))
        {
            Debug.LogWarning($"Key: {key} already present in the service pool.");
            return;
        }
        services.Add(key, service);
    }

    public void Remove(AvailableService key)
    {
        if (!services.ContainsKey(key))
        {
            Debug.LogWarning($"Key: {key} is not present in the service pool.");
            return;
        }
        services.Remove(key);
    }

    public IService Get(AvailableService key)
    {
        var retrievedService = services[key];

        if (retrievedService == null)
        {
            Debug.LogWarning($"Key: {key} did not return a valid service.");
            return null;
        }
        return retrievedService;
    }
}
