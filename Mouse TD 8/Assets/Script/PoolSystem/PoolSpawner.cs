using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PoolSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefabEnnemi;
    public List<NumberEnnemi> _waves = new List<NumberEnnemi>();
    [SerializeField] private TextMeshProUGUI _textWave;
    
    private Map _tilesMap;
    private ComponentPool<Enemy> _poolEnnemi;
    private bool _start;
    [HideInInspector] public int _waveIndex;
    private const int preAllocationCount = 50;
    private Button _buttonStart;

    public static PoolSpawner Instance;
    private void Awake()
    {
        _poolEnnemi = new ComponentPool<Enemy>(_prefabEnnemi, 500, preAllocationCount);
        _tilesMap = GetComponent<Map>();
        _textWave.text = "Wave " + 1 + " / " + _waves.Count;
        Instance = this;
    }
    private void Update()
    {
        if (_poolEnnemi.AliveObjectCount > 0)
            return;
        
        _start = false;
        if (_buttonStart == null) return;
        
        _buttonStart.interactable = true;
        
        if (_waveIndex - 1 == _waves.Count && Life.instance.GetLife() > 0) EndGame.Instance.SetEndGame(true);
        if (PlayerPrefs.GetInt("AutoPlay") == 1) StartSpawn(_buttonStart);
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
        foreach (LevelAndNumberEnnemi t in wave.enemy)
        {
            for (int j = 0; j < t.number; j++)
            {
                Enemy enemy = _poolEnnemi.Get();
                enemy.transform.position = transform.position + Vector3.back * (1.5f * j);
                enemy.SetDistanceTraveled(-1.5f * j);
                enemy.SetTilesMap(_tilesMap._tilesMap);
                enemy.GetComponent<EnemyLife>().SetColor(t.level - 1);
            }
        }
    }
}

[System.Serializable]
public class NumberEnnemi
{
    public List<LevelAndNumberEnnemi> enemy;
}
[System.Serializable]
public class LevelAndNumberEnnemi
{
    public int level;
    public int number;
}