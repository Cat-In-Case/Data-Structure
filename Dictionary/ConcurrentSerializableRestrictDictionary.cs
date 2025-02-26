using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using ArchiementTest;
using UnityEngine;

//Immutable할 이유가 없음
public sealed class ConcurrentSerializableRestrictDictionary<Key, Value> : IDictionary<Key, Value>, ISerializationCallbackReceiver, IReadOnlyDictionary<Key, Value>
    where Key : Condition, new()
{
    //Serializable
#if UNITY_EDITOR
    [System.Serializable]
    private struct KVP<Key, Value>
    {
        internal KVP(KeyValuePair<Key, Value> pair)
        {
            key = pair.Key;
            value = pair.Value;
        }
        internal Key key; internal Value value;
    }
    [Header("Immutable Data(Can not change)")]
    [SerializeField] private List<KVP<Key, Value>> _serializedData;
    public void OnBeforeSerialize()     // Dic => List
    {
        if (_serializedData == null || _internal == null)
            return;
        _serializedData.Clear();
        IEnumerable<KeyValuePair<Key, Value>> ienumerable = _internal;
        foreach(var kvp in ienumerable)
        {
            _serializedData.Add(new KVP<Key, Value>(kvp));
        }
    }
    public void OnAfterDeserialize()    //List => Dic
    {
        if (_serializedData == null || _internal == null)
            return;
        _internal.Clear();
        for(int i = 0;  i < _serializedData.Count; i++)
        {
            if (_internal.TryAdd(_serializedData[i].key, _serializedData[i].value) == false)
            {
                throw new System.ArgumentException("KeyValuePair Error");
            }
        }
    }

#else
    public void OnAfterDeserialize() { }
    public void OnBeforeSerialize() { }
#endif

    private readonly ConcurrentDictionary<Key, Value> _internal;        //느려터짐..

    //Type Restricted Funcs
    public Value this[Key key] { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public ICollection<Key> Keys => throw new System.NotImplementedException();

    public ICollection<Value> Values => throw new System.NotImplementedException();

    public int Count => throw new System.NotImplementedException();

    public bool IsReadOnly => throw new System.NotImplementedException();

    IEnumerable<Key> IReadOnlyDictionary<Key, Value>.Keys => throw new System.NotImplementedException();

    IEnumerable<Value> IReadOnlyDictionary<Key, Value>.Values => throw new System.NotImplementedException();

    public void Add(Key key, Value value)
    {

    }

    public void Add(KeyValuePair<Key, Value> item)
    {

    }

    public void Clear()
    {

    }

    public bool Contains(KeyValuePair<Key, Value> item)
    {

    }

    public bool ContainsKey(Key key)
    {
 
    }

    public void CopyTo(KeyValuePair<Key, Value>[] array, int arrayIndex)
    {

    }

    public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator()
    {

    }


    public bool Remove(Key key)
    {

    }

    public bool Remove(KeyValuePair<Key, Value> item)
    {

    }

    public bool TryGetValue(Key key, out Value value)
    {

    }

    IEnumerator IEnumerable.GetEnumerator()
    {

    }
}
