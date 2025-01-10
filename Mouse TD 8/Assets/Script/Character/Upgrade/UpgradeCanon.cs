public class UpgradeCanon : BaseUpgrade
{
    public override void OnUpgrade(DefenseStat stat, int level)
    {
        switch (level)
        {
            case 1:
                stat._speedAttack *= 0.7f;
                break;
            case 2:
                stat._speedAttack *= 0.5f;
                break;
        }
    }
}
