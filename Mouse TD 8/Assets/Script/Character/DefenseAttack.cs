using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DefenseAttack : MonoBehaviour
{
    public bool _canAttack;
    private DefenseStat _defenseStat;
    private float _timer;
    private readonly Collider[] _ennemis = new Collider[50];
    private Transform _transform;
    private void Start()
    {
        _transform = transform;
        _defenseStat = GetComponent<DefenseStat>();
        if (_defenseStat == null)
            _defenseStat = gameObject.AddComponent<DefenseStat>();
    }

    private void Update()
    {
        if (!_canAttack) return;
        
        _timer += Time.deltaTime;
        if (_timer < _defenseStat._speedAttack) return;
        
        _timer = 0;
        int size = Physics.OverlapSphereNonAlloc(_transform.position, _defenseStat._radiusAttack, _ennemis, _defenseStat._includeLayer);
        
        if (size <= 0) return;
        
        if (_defenseStat._areaAttack)
        {
            
        }
        else
        {
            Ennemi ennemiFirst = GetFirstEnnemi(_ennemis.Take(size).Select(t => t.GetComponentInParent<Ennemi>()).ToList());
            Attack(ennemiFirst, _defenseStat);
        }
    }
    private static void Attack(Ennemi ennemi, DefenseStat defenseStat)
    {
        EnnemiLife ennemiLife = ennemi.GetComponent<EnnemiLife>();
        ennemiLife.RemoveLife(defenseStat._damage);
    }
    private static Ennemi GetFirstEnnemi(List<Ennemi> ennemiList)
    {
        foreach (Ennemi t in ennemiList)
        {
            print(t);
        }
        Ennemi ennemiFirst = ennemiList[0];
        foreach (Ennemi t in ennemiList.Where(t => ennemiFirst.GetDistanceTraveled() < t.GetDistanceTraveled()))
        {
            ennemiFirst = t;
        }
        return ennemiFirst;
    }
}
