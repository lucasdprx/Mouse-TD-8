using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
public class PoolSpawner : MonoBehaviour
{
    [FormerlySerializedAs("_prefabEnnemi")]
    
    [SerializeField] private GameObject _prefabEnnemi;
    [SerializeField] private List<NumberEnnemi> _waves = new List<NumberEnnemi>();
    
    private Map _tilesMap;
    private ComponentPool<Ennemi> _poolEnnemi;
    private bool _start;
    private int _waveIndex = 0;
    private const int preAllocationCount = 50;
    private void Awake()
    {
        _poolEnnemi = new ComponentPool<Ennemi>(_prefabEnnemi, 50, preAllocationCount);
        _tilesMap = GetComponent<Map>();
    }
    private void Update()
    {
        if (_poolEnnemi.AliveObjectCount <= 0)
        {
            _start = false;
        }
    }
    public void StartSpawn(bool value)
    {
        if (_start) return;
        
        _waveIndex++;
        _start = value;
        SpawnEnnemi(_waveIndex - 1);
    }
    private void SpawnEnnemi(int waveIndex = 0)
    {
        if (waveIndex >= _waves.Count)
            return;
        
        NumberEnnemi wave = _waves[waveIndex];
        for (int i = 0; i < wave.levelEnnemi.Count; i++)
        {
            for (int j = 0; j < wave.numberEnnemi[i]; j++)
            {
                Ennemi ennemi = _poolEnnemi.Get();
                ennemi.transform.position = transform.position + Vector3.back * 1.5f * j;
                ennemi.SetDistanceTraveled(-1.5f * j);
                ennemi.SetTilesMap(_tilesMap._tilesMap);
                ennemi._ennemiLife.SetColor(wave.levelEnnemi[i] - 1);
            }
        }
    }
}

[System.Serializable]
public class NumberEnnemi
{
    public List<int> levelEnnemi;
    public List<int> numberEnnemi;
}