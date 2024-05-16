using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public abstract class ObjectPool<PooledObject> : MonoBehaviour where PooledObject : MonoBehaviour
{
    [SerializeField] private PooledObject _prefab;

    private Queue<PooledObject> _inactivePool = new Queue<PooledObject>();
    private Queue<PooledObject> _activePool = new Queue<PooledObject>();

    public event Action<Vector3> OnTransformChanged;
    public event Action<int, int, PooledObject> OnObjectCountChanged;
    public  PooledObject GetObjectFromPool()
    {
        PooledObject obj;

        if (_inactivePool.Count > 0)
        {
            obj = _inactivePool.Dequeue();
            obj.gameObject.SetActive(true);
            _activePool.Enqueue(obj);
            OnObjectCountChanged.Invoke(_inactivePool.Count, _activePool.Count, obj);
            return obj;
        }
        else
        {
            obj = Instantiate(_prefab);
            OnObjectCountChanged.Invoke(_inactivePool.Count, _activePool.Count, obj);
            return obj;
        }
        
    }

    public void ReturnObjectToPool(PooledObject obj)
    {
        OnTransformChanged?.Invoke(obj.transform.position);
        obj.gameObject.SetActive(false);

        if (_activePool.Contains(obj))
        {
            _activePool = new Queue<PooledObject>(_activePool.Where(item => item != obj));
        }

        _inactivePool.Enqueue(obj);
        OnObjectCountChanged.Invoke(_inactivePool.Count, _activePool.Count, obj);
    }
}
