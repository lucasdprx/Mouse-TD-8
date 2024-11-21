public interface IPoolObject<T> where T : class, IPoolObject<T>
{
    public void SetPool(Pool<T> pool);
}
