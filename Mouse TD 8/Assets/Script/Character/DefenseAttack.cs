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
    private readonly Collider[] _enemies = new Collider[50];
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
        
        int size = Physics.OverlapSphereNonAlloc(_transform.position, _defenseStat._radiusAttack, _enemies, _defenseStat._includeLayer);
        if (size <= 0) return;
        _timer = 0;

        List<Enemy> enemies = new List<Enemy>();
        for (int i = 0; i < size; i++)
            enemies.Add(_enemies[i].GetComponentInParent<Enemy>());
        
        Enemy enemyFirst = GetFirstEnemy(enemies);
        
        if (_defenseStat._areaAttack)
            AreaAttack(enemyFirst, _defenseStat);
        
        else if (_defenseStat._freezeAttack)
            FreezeAttack(enemies, _defenseStat);
        
        else if (_defenseStat._slowAttack)
            SlowAttack(enemies, _defenseStat);
        
        else
            SimpleAttack(enemyFirst, _defenseStat);
    }
    private void SlowAttack(List<Enemy> enemies, DefenseStat defenseStat)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            Enemy enemyFirst = GetFirstEnemy(enemies);
            if (enemyFirst._isSlow)
            {
                enemies.Remove(enemyFirst);
            }
            else
            {
                transform.DOLookAt(enemyFirst.transform.position, 0.25f);
                StartCoroutine(SlowEnnemi(enemyFirst, defenseStat));
            }
        }
    }
    private static IEnumerator SlowEnnemi(Enemy enemy, DefenseStat defenseStat)
    {
        float speed = enemy.GetSpeed();
        enemy.SetSpeed(speed * 0.5f);
        enemy._isSlow = true;
        yield return new WaitForSeconds(defenseStat._slowTime);
        enemy.SetSpeed(speed);
        enemy._isSlow = false;
    }
    private void FreezeAttack(List<Enemy> enemies, DefenseStat defenseStat)
    {
        foreach (Enemy enemy in enemies)
        {
            EnemyLife enemyLife = enemy.enemyLife;
            enemyLife.RemoveLife();
            if (enemyLife.GetIndexColor() < 0) continue;
            if (enemy._isFrozen) continue;
            
            StartCoroutine(FreezeEnnemi(enemy, defenseStat));
        }
    }
    private static IEnumerator FreezeEnnemi(Enemy enemy, DefenseStat defenseStat)
    {
        enemy.SetSpeed(0f);
        enemy._isFrozen = true;
        yield return new WaitForSeconds(defenseStat._freezeTime);
        enemy.enemyLife.SetColor(enemy.enemyLife.GetIndexColor());
    }
    private void SimpleAttack(Enemy enemy, DefenseStat defenseStat)
    {
        transform.DOLookAt(enemy.transform.position, 0.25f);
        EnemyLife enemyLife = enemy.GetComponent<EnemyLife>();
        enemyLife.RemoveLife();
    }
    private void AreaAttack(Enemy enemy, DefenseStat defenseStat)
    {
        Collider[] colliders = new Collider[50];
        int size = Physics.OverlapSphereNonAlloc(enemy.transform.position, defenseStat._radiusAreaAttack, colliders, defenseStat._includeLayer);
        if (size <= 0) return;
        transform.DOLookAt(enemy.transform.position, 0.25f);
        
        for (int i = 0; i < size; i++)
        {
            EnemyLife enemyLife = colliders[i].GetComponentInParent<EnemyLife>();
            if (enemyLife == null) continue;
            
            enemyLife.RemoveLife();
        }
    }
    private static Enemy GetFirstEnemy(List<Enemy> enemyList)
    {
        Enemy enemyFirst = enemyList[0];
        foreach (Enemy t in enemyList.Where(t => enemyFirst.GetDistanceTraveled() < t.GetDistanceTraveled()))
        {
            enemyFirst = t;
        }
        return enemyFirst;
    }
}
