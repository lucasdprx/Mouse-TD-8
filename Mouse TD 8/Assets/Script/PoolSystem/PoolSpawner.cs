using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PoolSpawner : MonoBehaviour
{
    [FormerlySerializedAs("_prefabEnnemi")]
    [SerializeField] private GameObject _prefabEnnemi;

    private ComponentPool<Ennemi> _poolEnnemi;
    [SerializeField] private int preAllocationCount = 10;

    private void Awake()
    {
        _poolEnnemi = new ComponentPool<Ennemi>(_prefabEnnemi, 10, preAllocationCount);
    }
    private void Start()
    {
        for (int i = 0; i < preAllocationCount; i++)
        {
            Ennemi ennemi = _poolEnnemi.Get();
            ennemi.transform.position = transform.position;
        }
    }
}