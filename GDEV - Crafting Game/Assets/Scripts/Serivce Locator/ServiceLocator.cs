using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator
{
    private readonly Dictionary<Type, IService> services = new();

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

    public void Add(IService service)
    {
        Type type = service.GetType();

        if (services.ContainsKey(type))
        {
            Debug.LogWarning($"Key: {type} already present in the service pool.");
            return;
        }
        services.Add(type, service);
    }

    public void Remove(Type type)
    {
        if (!services.ContainsKey(type))
        {
            Debug.LogWarning($"Key: {type} is not present in the service pool.");
            return;
        }
        services.Remove(type);
    }

    public T Get<T>() where T : IService
    {
        Type type = typeof(T);

        if (services.ContainsKey(type))
        {
            return (T)services[type];
        }
        else
        {
            Debug.LogWarning($"Key: {type} did not return a valid service.");
            return default;
        }
    }
}
