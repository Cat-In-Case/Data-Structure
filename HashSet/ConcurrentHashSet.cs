using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class ConcurrentHashSet<T> : ISerializationCallbackReceiver, IReadOnlyCollection<T>, IDisposable, IEnumerable<T>
{
    private ConcurrentDictionary<T, byte> _internal;

    public int Count => _internal.Keys.Count;

    public bool IsReadOnly => false;

    public ConcurrentHashSet()
    {
        _internal = new ConcurrentDictionary<T, byte>();

#if UNITY_EDITOR
        _HashSet = new List<T>();
#endif
    }

    public ConcurrentHashSet(List<T> values)
    {
        _internal = new ConcurrentDictionary<T, byte>(values.ToDictionary(v => v, v => (byte)0));

#if UNITY_EDITOR
        _HashSet = new List<T>();
#endif
    }
    public ConcurrentHashSet(Dictionary<T, byte> values)
    {
        _internal = new ConcurrentDictionary<T, byte>(values, EqualityComparer<T>.Default);

#if UNITY_EDITOR
        _HashSet = new List<T>();
#endif
    }

#if UNITY_EDITOR
    [SerializeField] private List<T> _HashSet;
    public void OnBeforeSerialize()
    {
        _HashSet.Clear();
        foreach (var item in _internal.Keys)
        {
            _HashSet.Add(item);
        }
    }
    public void OnAfterDeserialize()
    {
        _internal = new ConcurrentDictionary<T, byte>(_HashSet.ToDictionary(v => v, v => (byte)0));
    }
#else
    public void OnAfterDeserialize() { }
    public void OnBeforeSerialize() { }
#endif

    public void Dispose()
    {
        _internal.Clear();
#if UNITY_EDITOR
        _HashSet.Clear();
        _HashSet = null;
#endif
    }

    public bool Add(T item)
    {
        return _internal.TryAdd(item, 0);
    }
    public bool Remove(T item)
    {
        return _internal.TryRemove(item, out _);
    }
    public bool Contains(T item)
    {
        return _internal.ContainsKey(item);
    }
    public void Clear()
    {
        _internal.Clear();
    }

    public IEnumerable<T> GetItems() {  return _internal.Keys; }

    public IEnumerator<T> GetEnumerator()
    {
        return _internal.Keys.GetEnumerator();
    }



    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new System.NotImplementedException();
    }


}
