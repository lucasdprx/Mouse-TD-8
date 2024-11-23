using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

public class Pool<T> : IPool<T> where T : class, IPoolObject<T>
{
    private readonly Func<T> _createFunc;
    private readonly Stack<T> _pooledObjects;
    private readonly Action<T> _onGetFunc;
    private readonly Action<T> _onReleaseFunc;

    private int _aliveObjectsCount;

    public int PooledObjectCount => _pooledObjects.Count;

    public int AliveObjectCount => _aliveObjectsCount;

    public Pool(Func<T> createFunc, Action<T> onGetFunc, Action<T> onReleaseFunc, int capacity =  50, int preAllocationCount = 0)
    {
        Assert.IsNotNull(createFunc, "The createFunc cannot be null");
        Assert.IsTrue(capacity >= 1, "The capacity cannot be less than 1");
        Assert.IsTrue(preAllocationCount >= 0, "The preAllocationCount cannot be less than 0");

        _pooledObjects = new Stack<T>(capacity);
        _createFunc = createFunc;
        _onGetFunc = onGetFunc;
        _onReleaseFunc = onReleaseFunc;
        _aliveObjectsCount = 0;

        PreAllocatePooledObjects(preAllocationCount);
    }
    private void PreAllocatePooledObjects(int numberOfObjects)
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            T pooledObject = InstantiatePoolObject();
            _aliveObjectsCount++;
            Release(pooledObject);
        }
    }

    public T Get()
    {

        T pooledObject = _pooledObjects.Count > 0 ? _pooledObjects.Pop() : InstantiatePoolObject();

        _aliveObjectsCount++;
        _onGetFunc?.Invoke(pooledObject);
        return pooledObject;
    }

    private T InstantiatePoolObject()
    {
        T pooledObject = _createFunc.Invoke();
        Assert.IsNotNull(pooledObject, "The pooledObject cannot be null");

        pooledObject.SetPool(this);

        return pooledObject;
    }

    public void Release(T pooledObject)
    {
        Assert.IsNotNull(pooledObject, "The pooledObject cannot be null");

        _pooledObjects.Push(pooledObject);
        _aliveObjectsCount--;
        _onReleaseFunc?.Invoke(pooledObject);
    } 
}
