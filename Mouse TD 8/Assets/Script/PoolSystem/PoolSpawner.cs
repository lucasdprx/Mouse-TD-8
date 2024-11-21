using UnityEngine;
using UnityEngine.Serialization;

public class PoolSpawner : MonoBehaviour
{
    [FormerlySerializedAs("_prefabEnnemi")]
    [SerializeField] private GameObject _prefabEnnemi;
    private Map _tilesMap;

    private ComponentPool<Ennemi> _poolEnnemi;
    [SerializeField] private int preAllocationCount = 10;

    private void Awake()
    {
        _poolEnnemi = new ComponentPool<Ennemi>(_prefabEnnemi, 10, preAllocationCount);
        _tilesMap = GetComponent<Map>();
    }
    private void Start()
    {
        for (int i = 0; i < preAllocationCount; i++)
        {
            Ennemi ennemi = _poolEnnemi.Get();
            ennemi.transform.position = transform.position + Vector3.back * 1.25f * i;
            ennemi.SetTilesMap(_tilesMap._tilesMap);
        }
    }
}