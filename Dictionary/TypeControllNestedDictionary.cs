using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[System.Serializable]
public sealed class TypeControllNestedDictionary<K, V> 
{
    [SerializeField] private SerializableConcurrentDictioanry<Type, SerializableConcurrentDictioanry<K, V>> _internal;

    public TypeControllNestedDictionary()
    {
        IDictionary<Type, SerializableConcurrentDictioanry<K, V>> dic = new Dictionary<Type, SerializableConcurrentDictioanry<K, V>>();
        _internal = SerializableConcurrentDictioanry<Type, SerializableConcurrentDictioanry<K, V>>.Create(dic);
    }
    public ICollection<K> NestedKeys(Type type) { return _internal[type].Keys; }
    public ICollection<V> NestedValues(Type type) { return _internal[type].Values; }

    public IEnumerable<KeyValuePair<K, V>> NestedPair(Type type) { return _internal[type]; }

    public bool TryGetValue()
    {
        return _internal.TryGetValue(typeof(Type), out var value);
    }

    public bool ContainsKey(Type type)
    {
        if (typeof(K).IsAssignableFrom(type))  //K와 동일한지
        {
            return false;
        }
        return _internal.ContainsKey(type);
    }
    public bool NestedContainsKey(Type type, K k)
    {

    }

    public bool TryAdd(Type type, K k, V v)
    {
        if (typeof(K).IsAssignableFrom(type))  //K와 동일한지
            return false;

        if (_internal.ContainsKey(type) == false)
        {
            _internal.Add(type, SerializableConcurrentDictioanry<K, V>.Create(new Dictionary<K, V>()));
        }
        return _internal[type].TryAdd(k, v);
    }
    public bool TryAddOrUpdate()
    {
        if (typeof(K).IsAssignableFrom(type))  //K와 동일한지
        {
            return false;
        }
    }

    public bool TryRemove()
    {
        if (typeof(K).IsAssignableFrom(type))  //K와 동일한지
        {
            return false;
        }
    }

    public bool TryNestedUpdate()
    {
        if (typeof(K).IsAssignableFrom(type))  //K와 동일한지
        {
            return false;
        }
    }

    public bool TryUpdate()
    {
        if (typeof(K).IsAssignableFrom(type))  //K와 동일한지
        {
            return false;
        }
    }

    public bool TryClear()
    {
        if (typeof(K).IsAssignableFrom(type))  //K와 동일한지
        {
            return false;
        }
    }

}