using UnityEngine;

public abstract class Spawner<SpawnedObject> : MonoBehaviour where SpawnedObject : MonoBehaviour
{
    private readonly Vector3 SpawnRange = new Vector3(5f, 10f, 5f);

    [SerializeField] protected ObjectPool<SpawnedObject> ObjectPool;
    [SerializeField] protected float _spawnInterval = 1f;

    [SerializeField] private Platform[] spawnPlatforms; 

    private float _spawnTimer = 0f;

    protected virtual void Update()
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
        obj.transform.position = GetPosition(); 
    }

    protected virtual Vector3 GetPosition()
    {
        Platform platform = spawnPlatforms[Random.Range(0, spawnPlatforms.Length)];

        Vector3 spawnPosition = platform.transform.position + new Vector3(
                Random.Range(-SpawnRange.x, SpawnRange.x),
                SpawnRange.y,
                Random.Range(-SpawnRange.z, SpawnRange.z)
            );

        return spawnPosition;
    }
}
