using System;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi : MonoBehaviour, IPoolObject<Ennemi>
{
    private Pool<Ennemi> _pool;
    private Transform _tileTarget;
    private List<Transform> _tilesMap = new List<Transform>();
    private int _tileIndex;
    private float _distanceTraveled;
    private EnnemiLife _ennemiLife;

    [SerializeField] private float _speed;
    private void Start()
    {
        _ennemiLife = GetComponent<EnnemiLife>();
    }


    private void Update()
    {
        MoveEnnemi();
    }
    private void MoveEnnemi()
    {
        if (_tileTarget == null) return;
        
        transform.position = Vector3.MoveTowards(transform.position, _tileTarget.position, _speed * Time.deltaTime);
        _distanceTraveled += _speed * Time.deltaTime;
        
        if (transform.position != _tileTarget.position) return;
        
        _tileIndex++;
        if (_tileIndex >= _tilesMap.Count) return;
        
        _tileTarget = _tilesMap[_tileIndex];
    }
    public void SetTilesMap(List<Transform> tilesMap)
    {
        _tilesMap = tilesMap;
        _tileTarget = _tilesMap[0];
        _tileIndex = 0;
    }
    public float GetDistanceTraveled() => _distanceTraveled;
    public void ResetDistanceTraveled() => _distanceTraveled = 0;
    public Pool<Ennemi> GetPool() => _pool;
    public void SetPool(Pool<Ennemi> pool)
    {
        if (pool == null) return;

        _pool = pool;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("End"))
            return;
        
        _ennemiLife.ResetPV();
        _pool.Release(this);
    }
}