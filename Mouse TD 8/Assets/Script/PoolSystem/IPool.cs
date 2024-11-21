public interface IPool<T>
{
    int PooledObjectCount { get; }
    int AliveObjectCount { get; }
    T Get();
    void Release(T pooledObject);
}