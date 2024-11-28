using System;
using UnityEngine;

public class ShootEffect : MonoBehaviour
{
    [SerializeField] private GameObject _shootEffectPrefab; //prefab
    [SerializeField] private float _speed = 22f;
    [SerializeField] private float _duration = 0.08f;
    
    private float _timer = 0f;
    private GameObject _shootEffect;

    private void Awake()
    {
        if (_shootEffectPrefab == null)
        {
            enabled = false;
            return;
        }
        
        _shootEffect = Instantiate(_shootEffectPrefab, transform.position, Quaternion.identity);
        _shootEffect.SetActive(false);
        enabled = false;
    }
    private void OnEnable()
    {
        _shootEffect.transform.position = transform.position;
        _shootEffect.SetActive(true);
    }
    private void Update()
    {
        _shootEffect.transform.position += transform.forward * (_speed * Time.deltaTime);
        _timer += Time.deltaTime;
        if (_timer < _duration) return;
        
        _timer = 0f;
        _shootEffect.SetActive(false);
        enabled = false;
        _shootEffect.transform.position = transform.position;
    }
}
