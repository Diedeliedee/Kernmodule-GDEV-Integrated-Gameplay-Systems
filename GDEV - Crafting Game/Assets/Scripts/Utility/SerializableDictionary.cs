using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableKeyValuePair<Tkey, TValue>
{
    public Tkey Key;
    public TValue Value;

    public SerializableKeyValuePair(Tkey _key, TValue _value)
    {
        Key = _key;
        Value = _value;
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
                keys.Add(pair.Key);
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
                values.Add(pair.Value);
            }

            return values;
        }
    }

    public TValue this[TKey _key]
    {
        get { return GetValue(_key); }
        set { SetValue(_key, value); }
    }

    public void Add(TKey _key, TValue _value)
    {
        pairs.Add(new SerializableKeyValuePair<TKey, TValue>(_key, _value));
    }

    public void Remove(TKey _key)
    {
        int index = Keys.IndexOf(_key);
        pairs.RemoveAt(index);
    }

    public TValue GetValue(TKey _getKey)
    {
        foreach (TKey key in Keys)
        {
            if (EqualityComparer<TKey>.Default.Equals(key, _getKey))
            {
                return pairs[Keys.IndexOf(key)].Value;
            }
        }

        return default;
    }

    public void SetValue(TKey _getKey, TValue _setValue)
    {
        foreach (TKey key in Keys)
        {
            if (EqualityComparer<TKey>.Default.Equals(key, _getKey))
            {
                pairs[Keys.IndexOf(key)].Value = _setValue;
            }
        }
    }

    public TKey GetKey(TValue _getValue)
    {
        foreach (TValue value in Values)
        {
            if (EqualityComparer<TValue>.Default.Equals(value, _getValue))
            {
                return pairs[Values.IndexOf(value)].Key;
            }
        }

        return default;
    }

    public bool ContainsKey(TKey _compareKey)
    {
        foreach (TKey key in Keys)
        {
            if (EqualityComparer<TKey>.Default.Equals(key, _compareKey))
            {
                return true;
            }
        }

        return false;
    }

    public bool ContainsValue(TValue _compareValue)
    {
        foreach (TValue value in Values)
        {
            if (EqualityComparer<TValue>.Default.Equals(value, _compareValue))
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
            dictionary.Add(pair.Key, pair.Value);
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
