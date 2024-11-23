using System.Collections.Generic;
using UnityEngine;

public class EnnemiLife : MonoBehaviour
{
    private Ennemi _ennemi;
    private readonly List<EnnemiStat> _colors = new List<EnnemiStat>
    {
        new EnnemiStat { color = Color.red, speed = 2, life = 1 },
        new EnnemiStat { color = Color.blue, speed = 4, life = 1},
        new EnnemiStat { color = Color.green, speed = 6, life = 2 },
        new EnnemiStat { color = Color.cyan, speed = 8, life = 3 },
        new EnnemiStat { color = Color.magenta, speed = 10, life = 5 },
        new EnnemiStat { color = Color.black, speed = 15, life = 10 },
    };
    private int _indexColor = 0;

    private void Awake()
    {
        _ennemi = GetComponent<Ennemi>();
    }
    public void SetColor(int index)
    {
        _indexColor = index;
        _ennemi.GetComponentInChildren<MeshRenderer>().material.color = _colors[_indexColor].color;
        _ennemi.SetSpeed(_colors[_indexColor].speed);
    }

    public void RemoveLife()
    {
        _colors[_indexColor].life--;
        if (_colors[_indexColor].life > 0) return;
        
        _indexColor--;
        if (_indexColor < 0)
        {
            _ennemi.GetPool().Release(_ennemi);
            _ennemi.ResetDistanceTraveled();
            return;
        }

        SetColor(_indexColor);
    }
}
