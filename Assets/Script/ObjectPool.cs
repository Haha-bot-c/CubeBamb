using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class ObjectPool<PooledObject> : MonoBehaviour where PooledObject : MonoBehaviour
{
    [SerializeField] private PooledObject _prefab;

    private Queue<PooledObject> _pool = new Queue<PooledObject>();
    
    public event Action<Vector3> OnTransformChanged;

    public  PooledObject GetObjectFromPool()
    {
        PooledObject obj;

        if (_pool.Count > 0)
        {
            obj = _pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            obj = Instantiate(_prefab);
            return obj;
        }
    }

    public void ReturnObjectToPool(PooledObject obj)
    {
        OnTransformChanged?.Invoke(obj.transform.position);
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }
}
