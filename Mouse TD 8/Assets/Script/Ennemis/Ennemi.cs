using System.Collections.Generic;
using UnityEngine;

public class Ennemi : MonoBehaviour, IPoolObject<Ennemi>
{
    private Pool<Ennemi> _pool;
    private Transform _tileTarget;
    private List<Transform> _tilesMap = new List<Transform>();
    private int _tileIndex;

    [SerializeField] private float _speed;
    
    public void SetTilesMap(List<Transform> tilesMap)
    {
        _tilesMap = tilesMap;
        _tileTarget = _tilesMap[0];
        _tileIndex = 0;
    }
    
    public void SetPool(Pool<Ennemi> pool)
    {
        if (pool == null) return;

        _pool = pool;
    }
    private void Update()
    {
        MoveEnnemi();
    }
    private void MoveEnnemi()
    {
        if (_tileTarget == null) return;
        
        transform.position = Vector3.MoveTowards(transform.position, _tileTarget.position, _speed * Time.deltaTime);
        
        if (transform.position != _tileTarget.position) return;
        
        _tileIndex++;
        if (_tileIndex >= _tilesMap.Count) return;
        
        _tileTarget = _tilesMap[_tileIndex];
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