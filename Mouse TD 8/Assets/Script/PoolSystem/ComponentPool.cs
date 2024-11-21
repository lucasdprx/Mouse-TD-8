using UnityEngine;
using UnityEngine.Assertions;

public class ComponentPool<T> : IPool<T> where T : Component, IPoolObject<T>
{
    private readonly Pool<T> _pool;
    public int PooledObjectCount
    {
        get { return _pool.PooledObjectCount; }
    }
    public int AliveObjectCount
    {
        get { return _pool.AliveObjectCount; }
    }

    public ComponentPool(GameObject prefab, int capacity = 50, int preAllocationCount = 0)
    {
        Assert.IsNotNull(prefab, "The prefab cannot be null");

        _pool = new Pool<T>(() =>
                                  { GameObject gameObject = Object.Instantiate(prefab);
                                    return gameObject.GetComponent<T>(); },

                (pooledObject) => { pooledObject.gameObject.SetActive(true); },

                (pooledObject) => { pooledObject.gameObject.SetActive(false); }, capacity, preAllocationCount);
    }
    public T Get()
    {
        return _pool.Get();
    }

    public void Release(T pooledObject)
    {
        _pool.Release(pooledObject);
    }
}