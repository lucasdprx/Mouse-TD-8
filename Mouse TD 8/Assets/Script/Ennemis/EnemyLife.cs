using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    private Enemy _enemy;
    private readonly List<EnemyStat> _colors = new List<EnemyStat>
    {
        //Enemy classic
        new EnemyStat { id = 0, nextId = -1, color = Color.red    , speed = 2, life = 1 },
        new EnemyStat { id = 1, nextId = 0 , color = Color.blue   , speed = 3, life = 1 },
        new EnemyStat { id = 2, nextId = 1 , color = Color.green  , speed = 4, life = 2 },
        new EnemyStat { id = 3, nextId = 2 , color = Color.cyan   , speed = 5, life = 3 },
        new EnemyStat { id = 4, nextId = 3 , color = Color.magenta, speed = 7, life = 5 },
        new EnemyStat { id = 5, nextId = 4 , color = Color.black  , speed = 3, life = 15 }
    };
    private List<EnemyStat> _colorsCopy = new List<EnemyStat>();
    private int _currentId;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }
    private void OnDisable()
    {
        _colorsCopy = _colors.ToList();
    }
    public void SetColor(int index)
    {
        if (index < 0 || index >= _colors.Count) return;
        
        _currentId = index;
        _enemy.GetComponentInChildren<MeshRenderer>().material.color = _colors[_currentId].color;
        _enemy.SetSpeed(_colors[_currentId].speed);
    }

    public void RemoveLife()
    {
        if (_currentId < 0 || _currentId >= _colors.Count) return;
        if (_colorsCopy.Count == 0) 
            _colorsCopy = _colors.ToList();
        
        EnemyStat enemyStat = _colorsCopy[_currentId];
        enemyStat.life--;
        _colorsCopy[_currentId] = enemyStat;
        if (enemyStat.life > 0) return;
        
        Money.instance.AddMoney(1);
        _currentId = _colorsCopy[_currentId].nextId;
        if (_currentId < 0)
        {
            _enemy.GetPool().Release(_enemy);
            return;
        }
        
        SetColor(_currentId);
    }

    public int GetId() => _colors[_currentId].id;
    public int GetCurrentId() => _currentId;
}
