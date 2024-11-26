using UnityEngine;
public class UpgradeFreeze : BaseUpgrade
{
    private SpriteRenderer _spriteRenderer;
    
    private void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    public override void OnUpgrade(DefenseStat stat, int level)
    {
        switch (level)
        {
            case 1:
                stat._radiusAttack *= 1.3f;
                _spriteRenderer.transform.localScale = Vector3.one * stat._radiusAttack;
                break;
            case 2:
                stat._radiusAttack *= 1.5f;
                _spriteRenderer.transform.localScale = Vector3.one * stat._radiusAttack;
                break;
            case 3:
                print("Upgrade 3");
                break;
            case 4:
                print("Upgrade 4");
                break;
        }
    }
}
