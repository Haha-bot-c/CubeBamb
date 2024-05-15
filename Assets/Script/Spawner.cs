using UnityEngine;

public abstract class Spawner<SpawnedObject> : MonoBehaviour where SpawnedObject : MonoBehaviour
{
    private readonly Vector3 SpawnRange = new Vector3(5f, 10f, 5f);

    [SerializeField] protected ObjectPool<SpawnedObject> ObjectPool;
    [SerializeField] private float _spawnInterval = 1f;

    private float _spawnTimer = 0f;

    private void Update()
    {
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= _spawnInterval)
        {
            SpawnObject();
            _spawnTimer = 0f;
        }
    }

    protected virtual void SpawnObject()
    {
        SpawnedObject obj = ObjectPool.GetObjectFromPool();

        if (obj != null)
        {
            obj.transform.position = GetPosotion();
        }
    }

    protected virtual Vector3 GetPosotion()
    {
        Vector3 spawnPosition = transform.position + new Vector3(
                Random.Range(-SpawnRange.x, SpawnRange.x),
                SpawnRange.y,
                Random.Range(-SpawnRange.z, SpawnRange.z)
            );

        return spawnPosition;
    }
}
