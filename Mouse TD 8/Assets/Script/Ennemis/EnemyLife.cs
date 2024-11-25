using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    private Enemy _enemy;
    private readonly List<EnemyStat> _colors = new List<EnemyStat>
    {
        new EnemyStat { color = Color.red, speed = 2, life = 1 },
        new EnemyStat { color = Color.blue, speed = 3, life = 1 },
        new EnemyStat { color = Color.green, speed = 5, life = 2 },
        new EnemyStat { color = Color.cyan, speed = 7, life = 3 },
        new EnemyStat { color = Color.magenta, speed = 9, life = 5 },
        new EnemyStat { color = Color.black, speed = 12, life = 10 },
    };
    private int _indexColor = 0;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
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
        _colors[_indexColor].life--;
        if (_colors[_indexColor].life > 0) return;
        
        _indexColor--;
        Money.instance.AddMoney(1);
        if (_indexColor < 0)
        {
            _enemy.GetPool().Release(_enemy);
            _enemy.ResetDistanceTraveled();
            _enemy.transform.position = Vector3.zero;
            return;
        }
        
        SetColor(_indexColor);
    }
    public int GetIndexColor() => _indexColor;
}
