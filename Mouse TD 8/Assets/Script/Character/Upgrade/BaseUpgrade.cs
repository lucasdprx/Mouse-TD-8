using UnityEngine;

public abstract class BaseUpgrade : MonoBehaviour
{
    public abstract void OnUpgrade(DefenseStat stat, int level);
}
