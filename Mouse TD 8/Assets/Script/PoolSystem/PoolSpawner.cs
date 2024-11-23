using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class PoolSpawner : MonoBehaviour
{
    [FormerlySerializedAs("_prefabEnnemi")]
    
    [SerializeField] private GameObject _prefabEnnemi;
    [SerializeField] private List<NumberEnnemi> _waves = new List<NumberEnnemi>();
    [SerializeField] private TextMeshProUGUI _textWave;
    
    private Map _tilesMap;
    private ComponentPool<Ennemi> _poolEnnemi;
    private bool _start;
    private int _waveIndex = 0;
    private const int preAllocationCount = 50;
    private Button _buttonStart;
    private void Awake()
    {
        _poolEnnemi = new ComponentPool<Ennemi>(_prefabEnnemi, 50, preAllocationCount);
        _tilesMap = GetComponent<Map>();
        _textWave.text = "Wave " + 1 + " / " + _waves.Count;
    }
    private void Update()
    {
        if (_poolEnnemi.AliveObjectCount > 0)
            return;
        
        _start = false;
        if (_buttonStart == null) return;
        
        _buttonStart.interactable = true;
    }
    public void StartSpawn(Button buttonStart)
    {
        if (_start || _waveIndex >= _waves.Count) return;
        
        _buttonStart = buttonStart;
        _buttonStart.interactable = false;
        _waveIndex++;
        _start = true;
        SpawnEnnemi(_waveIndex - 1);
        _textWave.text = "Wave " + _waveIndex + " / " + _waves.Count;
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