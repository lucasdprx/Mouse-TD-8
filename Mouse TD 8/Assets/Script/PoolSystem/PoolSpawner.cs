using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class PoolSpawner : MonoBehaviour
{
    [FormerlySerializedAs("_prefabEnnemi")]
    
    [SerializeField] private GameObject _prefabEnnemi;
    [SerializeField] private int preAllocationCount = 10;
    [SerializeField] private List<Ennemi> _ennemiList = new List<Ennemi>();
    
    private Map _tilesMap;
    private ComponentPool<Ennemi> _poolEnnemi;
    private const float _spawnRate = 0.1f;
    private float _timer;
    private void Awake()
    {
        _poolEnnemi = new ComponentPool<Ennemi>(_prefabEnnemi, 50, preAllocationCount);
        _tilesMap = GetComponent<Map>();
    }
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer < _spawnRate) return;
        _timer = 0;
        
        Ennemi ennemi = _poolEnnemi.Get();
        ennemi.transform.position = transform.position;
        ennemi.SetTilesMap(_tilesMap._tilesMap);
        _ennemiList.Add(ennemi);
    }
}