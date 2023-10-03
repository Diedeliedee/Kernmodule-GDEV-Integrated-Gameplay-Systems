using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableKeyValuePair<Tkey, TValue>
{
    public Tkey key;
    public TValue value;

    public SerializableKeyValuePair(Tkey key, TValue value)
    {
        this.key = key;
        this.value = value;
    }
}

[System.Serializable]
public class SerializableDictionary<TKey, TValue> : IEnumerable<SerializableKeyValuePair<TKey, TValue>>
{
    [SerializeField] private List<SerializableKeyValuePair<TKey, TValue>> pairs;

    [HideInInspector] public int Count { get { return pairs.Count; } }
    [HideInInspector] public List<TKey> Keys 
    { 
        get 
        {
            List<TKey> keys = new();
            foreach (SerializableKeyValuePair<TKey, TValue> pair in pairs)
            {
                keys.Add(pair.key);
            }

            return keys;
        } 
    }
    [HideInInspector] public List<TValue> Values 
    {
        get
        {
            List<TValue> values = new();
            foreach (SerializableKeyValuePair<TKey, TValue> pair in pairs)
            {
                values.Add(pair.value);
            }

            return values;
        }
    }

    public TValue this[TKey key]
    {
        get { return GetValue(key); }
        set { SetValue(key, value); }
    }

    public void Add(TKey key, TValue value)
    {
        pairs.Add(new SerializableKeyValuePair<TKey, TValue>(key, value));
    }

    public void Remove(TKey key)
    {
        int index = Keys.IndexOf(key);
        pairs.RemoveAt(index);
    }

    public TValue GetValue(TKey getKey)
    {
        foreach (TKey key in Keys)
        {
            if (EqualityComparer<TKey>.Default.Equals(key, getKey))
            {
                return pairs[Keys.IndexOf(key)].value;
            }
        }

        return default;
    }

    public void SetValue(TKey getKey, TValue setValue)
    {
        foreach (TKey key in Keys)
        {
            if (EqualityComparer<TKey>.Default.Equals(key, getKey))
            {
                pairs[Keys.IndexOf(key)].value = setValue;
            }
        }
    }

    public TKey GetKey(TValue getValue)
    {
        foreach (TValue value in Values)
        {
            if (EqualityComparer<TValue>.Default.Equals(value, getValue))
            {
                return pairs[Values.IndexOf(value)].key;
            }
        }

        return default;
    }

    public bool ContainsKey(TKey compareKey)
    {
        foreach (TKey key in Keys)
        {
            if (EqualityComparer<TKey>.Default.Equals(key, compareKey))
            {
                return true;
            }
        }

        return false;
    }

    public bool ContainsValue(TValue compareValue)
    {
        foreach (TValue value in Values)
        {
            if (EqualityComparer<TValue>.Default.Equals(value, compareValue))
            {
                return true;
            }
        }

        return false;
    }

    public void Clear()
    {
        pairs.Clear();
    }

    public Dictionary<TKey, TValue> ToDictionary()
    {
        Dictionary<TKey, TValue> dictionary = new();

        foreach (SerializableKeyValuePair<TKey, TValue> pair in pairs)
        {
            dictionary.Add(pair.key, pair.value);
        }

        return dictionary;
    }

    public IEnumerator<SerializableKeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return ((IEnumerable<SerializableKeyValuePair<TKey, TValue>>)pairs).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)pairs).GetEnumerator();
    }
}
