using System;
using UnityEngine;

namespace Utility.System.Object_Pooler_System
{
    [Serializable]
    public struct ObjectPoolItem
    {
        public GameObject objectToPool;
        public int amountToPool;
        public PoolItemType poolItemType;
    }
}