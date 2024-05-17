public class CubeSpawner : Spawner<Cube>    
{
    protected override void SpawnObject()
    {
        Cube cube = ObjectPool.GetObjectFromPool();

        if (cube != null)
        {
            cube.transform.position = GetPosition();
            cube.ReturnedToPool += CubeOnReturnedToPool;
        }
    }

    private void CubeOnReturnedToPool(Cube obj)
    {
        obj.ReturnedToPool -= CubeOnReturnedToPool;
        ObjectPool.ReturnObjectToPool(obj);
    }
}
