using TMPro;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TextUI<PooledObject> : MonoBehaviour where PooledObject : MonoBehaviour
{
    [SerializeField] private int _maxDisplayedObjects = 20;
    [SerializeField] private TMP_Text _totalObjectsText;
    [SerializeField] private TMP_Text _activeObjectsText;
    [SerializeField] private ObjectPool<PooledObject> _objectPool;
    private List<PooledObject> _spawnedObjects = new List<PooledObject>();

    private void OnEnable()
    {
        _objectPool.OnObjectCountChanged += HandleObjectCountChanged;
    }

    private void OnDisable()
    {
        _objectPool.OnObjectCountChanged -= HandleObjectCountChanged;
    }

    private void HandleObjectCountChanged(int inactiveCount, int activeCount, PooledObject obj)
    {
        if (_spawnedObjects.Contains(obj) == false)
        {
            _spawnedObjects.Add(obj);
        }

        _totalObjectsText.text = $"Всего объектов типа {obj.GetType()} : {inactiveCount + activeCount}. Неактивных {inactiveCount}. Активных {activeCount}";
        UpdateDisplayedObjects();
    }

    private void UpdateDisplayedObjects()
    {
        _activeObjectsText.text = "";
        int count = _spawnedObjects.Count;

        for (int i = count - 1; i >= 0; i--)
        {
            PooledObject obj = _spawnedObjects.ElementAt(i);
            string uniqueName = obj.GetInstanceID().ToString();
            _activeObjectsText.text += $"{obj.GetType()} : {uniqueName} - {(obj.gameObject.activeSelf ? "активен" : "неактивен")}\n";
        }
    }
}
