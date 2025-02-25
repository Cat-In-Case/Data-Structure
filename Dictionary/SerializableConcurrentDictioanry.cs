using System.Collections;
using UnityEngine;

namespace Assets.Resourc.___자료구조
{
    public class SerializableConcurrentDictioanry<K, V> : MonoBehaviour
    {

        [System.Serializable]
        private struct KVP<K, V>
        {
            public K key; public V value;
        }
    }
}