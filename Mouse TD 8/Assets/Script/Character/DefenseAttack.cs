using DG.Tweening;
using System.Collections;
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

        List<Ennemi> ennemis = new List<Ennemi>();
        for (int i = 0; i < size; i++)
            ennemis.Add(_ennemis[i].GetComponentInParent<Ennemi>());
        
        Ennemi ennemiFirst = GetFirstEnnemi(ennemis);
        
        if (_defenseStat._areaAttack)
        {
            AreaAttack(ennemiFirst, _defenseStat);
        }
        else if (_defenseStat._freezeAttack)
        {
            FreezeAttack(ennemis, _defenseStat);
        }
        else if (_defenseStat._slowAttack)
        {
            SlowAttack(ennemis, _defenseStat);
        }
        else
        {
            SimpleAttack(ennemiFirst, _defenseStat);
        }
    }
    private void SlowAttack(List<Ennemi> ennemis, DefenseStat defenseStat)
    {
        for (int i = 0; i < ennemis.Count; i++)
        {
            Ennemi ennemiFirst = GetFirstEnnemi(ennemis);
            if (ennemiFirst._isSlow)
            {
                ennemis.Remove(ennemiFirst);
            }
            else
            {
                transform.DOLookAt(ennemiFirst.transform.position, 0.25f);
                StartCoroutine(SlowEnnemi(ennemiFirst, defenseStat));
            }
        }
    }
    private static IEnumerator SlowEnnemi(Ennemi ennemi, DefenseStat defenseStat)
    {
        float speed = ennemi.GetSpeed();
        ennemi.SetSpeed(speed * 0.5f);
        ennemi._isSlow = true;
        yield return new WaitForSeconds(defenseStat._slowTime);
        ennemi.SetSpeed(speed);
        ennemi._isSlow = false;
    }
    private void FreezeAttack(List<Ennemi> ennemis, DefenseStat defenseStat)
    {
        foreach (Ennemi ennemi in ennemis)
        {
            EnnemiLife ennemiLife = ennemi._ennemiLife;
            ennemiLife.RemoveLife();
            if (ennemiLife.GetIndexColor() < 0) continue;
            
            StartCoroutine(FreezeEnnemi(ennemi, defenseStat));
        }
    }
    private static IEnumerator FreezeEnnemi(Ennemi ennemi, DefenseStat defenseStat)
    {
        float speed = ennemi.GetSpeed();
        ennemi.SetSpeed(0f);
        ennemi._isFrozen = true;
        yield return new WaitForSeconds(defenseStat._freezeTime);
        ennemi.SetSpeed(speed);
        ennemi._isFrozen = false;
    }
    private void SimpleAttack(Ennemi ennemi, DefenseStat defenseStat)
    {
        transform.DOLookAt(ennemi.transform.position, 0.25f);
        EnnemiLife ennemiLife = ennemi.GetComponent<EnnemiLife>();
        ennemiLife.RemoveLife();
    }
    private void AreaAttack(Ennemi ennemi, DefenseStat defenseStat)
    {
        Collider[] colliders = new Collider[50];
        int size = Physics.OverlapSphereNonAlloc(ennemi.transform.position, defenseStat._radiusAreaAttack, colliders, defenseStat._includeLayer);
        if (size <= 0) return;
        transform.DOLookAt(ennemi.transform.position, 0.25f);
        
        for (int i = 0; i < size; i++)
        {
            EnnemiLife ennemiLife = colliders[i].GetComponentInParent<EnnemiLife>();
            if (ennemiLife == null) continue;
            
            ennemiLife.RemoveLife();
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
