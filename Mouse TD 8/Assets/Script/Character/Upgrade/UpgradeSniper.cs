public class UpgradeSniper : BaseUpgrade
{

    public override void OnUpgrade(DefenseStat stat, int level)
    {
        switch (level)
        {
            case 1:
                stat._speedAttack /= 1.3f;
                break;
            case 2:
                stat._speedAttack /= 1.5f;
                break;
        }
    }
}
