
using UnityEngine;

public class SpawnerBomb : MonoBehaviour
{
    [SerializeField] private ObjectPool<Bomb> _poolBomb;
    [SerializeField] private ObjectPool<Cube> _cubePool;
    [SerializeField] private float _spawnInterval = 1f;

    private void OnEnable()
    {
        _cubePool.OnTransformChanged += SpawnObject;
    }

    private void OnDisable()
    {
        _cubePool.OnTransformChanged -= SpawnObject;
    }

    private void SpawnObject(Vector3 position)
    {
        Bomb bomb = _poolBomb.GetObjectFromPool();
       
        if (bomb != null)
        {
            bomb.transform.position = position;
            bomb.ReloadBomb();
            StartCoroutine(bomb.FadeOut());
            bomb.OnReturnedToPool += BombOnReturnedToPool;
        }
    }

    private void BombOnReturnedToPool(Bomb bomb)
    {
        bomb.OnReturnedToPool -= BombOnReturnedToPool; 
        _poolBomb.ReturnObjectToPool(bomb);
    }
}
