using UnityEngine;

public class DefenseStat : MonoBehaviour
{
    public float _damage = 10f;
    public float _radiusAttack = 2f;
    public float _speedAttack = 0.25f;
    public bool _areaAttack = false;
    public LayerMask _includeLayer;
}
