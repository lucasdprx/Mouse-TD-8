using UnityEngine;

public class Ennemi : MonoBehaviour, IPoolObject<Ennemi>
{
    private Pool<Ennemi> _pool;
    public void SetPool(Pool<Ennemi> pool)
    {
        if (pool == null) return;

        _pool = pool;
    }

    private void OnTriggerEnter(Collider other)
    {
        print("OnTriggerEnter");
        if (other.gameObject.layer == LayerMask.NameToLayer("End"))
        {
            _pool.Release(this);
        }
    }
}