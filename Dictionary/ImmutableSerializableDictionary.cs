using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ImmutableSerializableDictionary<K, V> : ISerializationCallbackReceiver
{
#if UNITY_EDITOR
    [System.Serializable]
    private struct KVP<K, V>
    {
        private KVP(KeyValuePair<K, V> pair)
        {
            key = pair.Key;
            value = pair.Value;
        }
        public K key; public V value;
    }
    [Header("Immutable Data(Can not change)")]
    [SerializeField] private List<KVP<K, V>> _serializedData;
    public void OnBeforeSerialize()
    {

    }
    public void OnAfterDeserialize() 
    {
        
    }

#else
    public void OnAfterDeserialize() { }
    public void OnBeforeSerialize() { }
#endif

    private readonly Dictionary<K, V> _internal;

    private ImmutableSerializableDictionary() { }       //단순 생성 차단



}
