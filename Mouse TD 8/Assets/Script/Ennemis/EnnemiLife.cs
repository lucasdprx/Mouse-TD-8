using System;
using UnityEngine;

public class EnnemiLife : MonoBehaviour
{
    [SerializeField] private float _pv = 100;
    [SerializeField] private float _pvMax = 100;
    private Ennemi _ennemi;

    private void Start()
    {
        _ennemi = GetComponent<Ennemi>();
    }
    public float GetPv() => _pv;
    public void ResetPV() => _pv = _pvMax;
    public void RemoveLife(float amount)
    {
        _pv -= amount;
        if (!(_pv <= 0))
            return;
        
        _ennemi.GetPool().Release(_ennemi);
        _ennemi.ResetDistanceTraveled();
        ResetPV();
    }
}
