using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DefenseAttack : MonoBehaviour
{
    public bool _canAttack;
    private DefenseStat _defenseStat;
    private float _timer;
    private Collider[] _ennemis = new Collider[50];
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

        List<Ennemi> ennemis = new List<Ennemi>();
        for (int i = 0; i < size; i++)
            ennemis.Add(_ennemis[i].GetComponentInParent<Ennemi>());
        
        Ennemi ennemiFirst = GetFirstEnnemi(ennemis);
        if (_defenseStat._areaAttack)
        {
            AreaAttack(ennemiFirst, _defenseStat);
        }
        else
        {
            transform.DOLookAt(ennemiFirst.transform.position, 0.25f);
            SimpleAttack(ennemiFirst, _defenseStat);
        }
    }
    private static void SimpleAttack(Ennemi ennemi, DefenseStat defenseStat)
    {
        EnnemiLife ennemiLife = ennemi.GetComponent<EnnemiLife>();
        ennemiLife.RemoveLife(defenseStat._damage);
    }
    private static void AreaAttack(Ennemi ennemi, DefenseStat defenseStat)
    {
        Collider[] colliders = new Collider[50];
        int size = Physics.OverlapSphereNonAlloc(ennemi.transform.position, defenseStat._radiusAreaAttack, colliders, defenseStat._includeLayer);
        if (size <= 0) return;

        for (int i = 0; i < size; i++)
        {
            EnnemiLife ennemiLife = colliders[i].GetComponentInParent<EnnemiLife>();
            if (ennemiLife == null) continue;
            
            ennemiLife.RemoveLife(defenseStat._damage);
        }
    }
    private static Ennemi GetFirstEnnemi(List<Ennemi> ennemiList)
    {
        Ennemi ennemiFirst = ennemiList[0];
        foreach (Ennemi t in ennemiList.Where(t => ennemiFirst.GetDistanceTraveled() < t.GetDistanceTraveled()))
        {
            ennemiFirst = t;
        }
        return ennemiFirst;
    }
}
