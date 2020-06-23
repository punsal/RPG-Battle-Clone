using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utility.System.Object_Pooler_System
{
    public class ObjectPooler : MonoBehaviour
    {
        public static ObjectPooler SharedInstance;

        [SerializeField] private List<ObjectPoolItem> itemsToPool = new List<ObjectPoolItem>();
        private List<GameObject> pooledObjects;

        private void Awake()
        {
            SharedInstance = this;
        }

        // Use this for initialization
        private void Start()
        {
            pooledObjects = new List<GameObject>();
            foreach (var item in itemsToPool)
                for (var i = 0; i < item.amountToPool; i++)
                {
                    var obj = Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                }
        }

        public GameObject GetPooledObject(string itemTag)
        {
            foreach (var pooledObject in pooledObjects.Where(pooledObject =>
                !pooledObject.activeInHierarchy && pooledObject.CompareTag(itemTag)))
                return pooledObject;
            foreach (var obj in from item in itemsToPool
                where item.objectToPool.CompareTag(itemTag)
                where item.poolItemType == PoolItemType.Expandable
                select Instantiate(item.objectToPool))
            {
                obj.SetActive(false);
                pooledObjects.Add(obj);
                return obj;
            }

            return null;
        }
    }
}