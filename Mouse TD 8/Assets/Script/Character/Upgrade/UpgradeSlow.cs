public class UpgradeSlow : BaseUpgrade
{
    public override void OnUpgrade(DefenseStat stat, int level)
    {
        switch (level)
        {
            case 1:
                stat._slowTime *= 1.3f;
                stat._speedAttack *= 0.75f;
                break;
            case 2:
                stat._slowTime *= 1.5f;
                stat._speedAttack *= 0.5f;
                break;
        }
    }
}
