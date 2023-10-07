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
    /// Add a service to the locator, using the passed in instance to generate a Type key.
    /// </summary>
    /// <param name="_service">The instance to be added as service to the locator.</param>
    /// <param name="_key">Optional type key overload. GetType() doesn't return superclasses or interfaces when passing in a subclass.</param>
    public void Add(IService _service, Type _key = null)
    {
        if (_key == null) { _key = _service.GetType(); }

        if (services.ContainsKey(_key))
        {
            Debug.LogWarning($"Key: {_key} already present in the service pool.");
            return;
        }
        services.Add(_key, _service);
    }

    /// <summary>
    /// Remove a service from the locator, using the Type key to identify which instance should be removed.
    /// </summary>
    public void Remove(Type _key)
    {
        if (!services.ContainsKey(_key))
        {
            Debug.LogWarning($"Key: {_key} is not present in the service pool.");
            return;
        }
        services.Remove(_key);
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
            Debug.LogError($"Key: {key} did not return a valid service.");
            return default;
        }
    }
}
