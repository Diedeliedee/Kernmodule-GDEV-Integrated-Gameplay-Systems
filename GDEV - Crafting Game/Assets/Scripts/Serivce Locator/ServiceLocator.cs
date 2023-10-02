using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator
{
    //  Service Item Pool:
    private readonly Dictionary<Type, IService> services = new();

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

    /// <summary>
    /// Add a service to the locator, using the passed in Type as key.
    /// </summary>
    public void Add(Type key, IService service)
    {
        if (services.ContainsKey(key))
        {
            Debug.LogWarning($"Key: {key} already present in the service pool.");
            return;
        }
        services.Add(key, service);
    }

    /// <summary>
    /// Remove a service from the locator, using the Type key to identify which instance should be removed.
    /// </summary>
    public void Remove(Type key)
    {
        if (!services.ContainsKey(key))
        {
            Debug.LogWarning($"Key: {key} is not present in the service pool.");
            return;
        }
        services.Remove(key);
    }

    /// <returns>A service attached to the locator, if an instance of the specified generic type is present.</returns>
    public T Get<T>() where T : IService
    {
        Type key = typeof(T);

        if (services.ContainsKey(key))
        {
            return (T)services[key];
        }
        else
        {
            Debug.LogWarning($"Key: {key} did not return a valid service.");
            return default;
        }
    }
}