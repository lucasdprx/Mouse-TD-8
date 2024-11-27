using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    private Enemy _enemy;
    private readonly List<EnemyStat> _colors = new List<EnemyStat>
    {
        new EnemyStat { color = Color.red, speed = 2, life = 1 },
        new EnemyStat { color = Color.blue, speed = 3, life = 1 },
        new EnemyStat { color = Color.green, speed = 4, life = 2 },
        new EnemyStat { color = Color.cyan, speed = 5, life = 3 },
        new EnemyStat { color = Color.magenta, speed = 7, life = 5 },
        new EnemyStat { color = Color.black, speed = 3, life = 15 }
    };
    private List<EnemyStat> _colorsCopy = new List<EnemyStat>();
    private int _indexColor = 0;

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
        
        _indexColor = index;
        _enemy.GetComponentInChildren<MeshRenderer>().material.color = _colors[_indexColor].color;
        _enemy.SetSpeed(_colors[_indexColor].speed);
    }

    public void RemoveLife()
    {
        if (_indexColor < 0 || _indexColor >= _colors.Count) return;
        if (_colorsCopy.Count == 0) 
            _colorsCopy = _colors.ToList();
        EnemyStat enemyStat = _colorsCopy[_indexColor];
        enemyStat.life--;
        _colorsCopy[_indexColor] = enemyStat;
        if (enemyStat.life > 0) return;
        
        _indexColor--;
        Money.instance.AddMoney(1);
        if (_indexColor < 0)
        {
            _enemy.GetPool().Release(_enemy);
            return;
        }
        
        SetColor(_indexColor);
    }
    public int GetIndexColor() => _indexColor;
}
