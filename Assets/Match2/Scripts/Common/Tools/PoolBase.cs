using System.Collections.Generic;
using UnityEngine;

namespace Match2.Scripts.Common.Tools
{
    /// <summary>
    /// Generic base class for all instance pools. Reduces memory allocations drastically by pooling instances, while possibly increasing the memory footprint.
    /// </summary>
    /// <typeparam name="T">Type of pooled instance.</typeparam>
    public abstract class PoolBase<T> where T : IPooled
    {
        private Queue<T> pool;
        private GameObject prefab;
        private GameObject host;
        
        public PoolBase(GameObject prefab, GameObject host, int initialInstanceCount)
        {
            this.pool = new Queue<T>(initialInstanceCount);
            this.prefab = prefab;
            this.host = host;
            
            for (int i = 0; i < initialInstanceCount; i++)
            {
                this.pool.Enqueue(CreateInstance());
            }
        }

        public int Count
        {
            get { return pool.Count; }
        }
        
        public T Get(Vector3 position, Quaternion rotation)
        {
            T entity;
            if (pool.Count > 0)
            {
                entity = pool.Dequeue();
            }
            else
            {
                entity = CreateInstance();
            }

            var t = entity.transform;

            t.position = position;
            t.rotation = rotation;

            entity.gameObject.SetActive(true);
            return entity;
        }
        
        public void Return(T entity)
        {
            entity.transform.SetParent(host.transform);
            entity.gameObject.SetActive(false);
            pool.Enqueue(entity);
        }

        private T CreateInstance()
        {
            var go = GameObject.Instantiate(prefab);
            go.transform.SetParent(host.transform);
            go.SetActive(false);
            return go.GetComponent<T>();
        }
    }
}