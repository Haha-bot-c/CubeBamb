public class CubeSpawner : Spawner<Cube>    
{
    protected override void SpawnObject()
    {
        Cube cube = ObjectPool.GetObjectFromPool();

        if (cube != null)
        {
            cube.transform.position = GetPosition();
            cube.OnReturnedToPool += CubeOnReturnedToPool;
        }
    }

    private void CubeOnReturnedToPool(Cube obj)
    {
        obj.OnReturnedToPool -= CubeOnReturnedToPool;
        ObjectPool.ReturnObjectToPool(obj);
    }
}
