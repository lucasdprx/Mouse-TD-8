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
        _timer = 0;
        int size = Physics.OverlapSphereNonAlloc(_transform.position, _defenseStat._radiusAttack, _enemies, _defenseStat._includeLayer);
        if (size <= 0) return;
        
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
        int count = enemies.Count;
        for (int i = 0; i < count; i++)
        {
            Enemy enemyFirst = GetFirstEnemy(enemies);
            if (enemyFirst._isSlow || enemyFirst._isFrozen)
            {
                enemies.Remove(enemyFirst);
                continue;
            }
            transform.DOLookAt(enemyFirst.transform.position, 0);
            ShootEffect shootEffect = defenseStat.GetComponent<ShootEffect>();
            if (shootEffect != null) 
                shootEffect.enabled = true;
            StartCoroutine(SlowEnnemi(enemyFirst, defenseStat));
            break;
        }
    }
    private static IEnumerator SlowEnnemi(Enemy enemy, DefenseStat defenseStat)
    {
        float speed = enemy.GetInitSpeed();
        enemy.SetSpeed(speed * defenseStat._slowMultiplier);
        enemy._isSlow = true;
        yield return new WaitForSeconds(defenseStat._slowTime);
        enemy.SetSpeed(speed);
        enemy._isSlow = false;
    }
    private void FreezeAttack(List<Enemy> enemies, DefenseStat defenseStat)
    {
        GlaceEffect glaceEffect = defenseStat.GetComponent<GlaceEffect>();
        foreach (Enemy enemy in enemies)
        {
            EnemyLife enemyLife = enemy.GetComponent<EnemyLife>();
            Enemy enemyFirst = GetFirstEnemy(enemies);
            if (glaceEffect != null)
                glaceEffect.StartEffect(defenseStat._radiusAttack);
            transform.DOLookAt(enemyFirst.transform.position, 0);
            enemyLife.RemoveLife();
            if (enemyLife.GetCurrentId() < 0) continue;
            if (enemy._isFrozen) continue;
            
            StartCoroutine(FreezeEnnemi(enemy, defenseStat));
        }
    }
    private static IEnumerator FreezeEnnemi(Enemy enemy, DefenseStat defenseStat)
    {
        float speed = enemy.GetInitSpeed();
        enemy.SetSpeed(0f);
        enemy._isFrozen = true;
        yield return new WaitForSeconds(defenseStat._freezeTime);
        enemy._isFrozen = false;
        enemy.SetSpeed(speed);
    }
    private void SimpleAttack(Enemy enemy, DefenseStat defenseStat)
    {
        transform.DOLookAt(enemy.transform.position, 0);
        EnemyLife enemyLife = enemy.GetComponent<EnemyLife>();
        enemyLife.RemoveLife();
        
        if (_defenseStat._particle != null)
            _defenseStat._particle.Play();
        
        ShootEffect shootEffect = defenseStat.GetComponent<ShootEffect>();
        if (shootEffect != null) 
            shootEffect.enabled = true;
    }
    private void AreaAttack(Enemy enemy, DefenseStat defenseStat)
    {
        Collider[] colliders = new Collider[50];
        int size = Physics.OverlapSphereNonAlloc(enemy.transform.position, defenseStat._radiusAreaAttack, colliders, defenseStat._includeLayer);
        if (size <= 0) return;
        transform.DOLookAt(enemy.transform.position, 0);
        
        for (int i = 0; i < size; i++)
        {
            EnemyLife enemyLife = colliders[i].GetComponentInParent<EnemyLife>();
            if (enemyLife == null) continue;
            
            enemyLife.RemoveLife();
        }
        
        ShootEffect shootEffect = defenseStat.GetComponent<ShootEffect>();
        if (shootEffect != null) 
            shootEffect.enabled = true;
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
