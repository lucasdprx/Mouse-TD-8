public class UpgradeKunai : BaseUpgrade
{
    public override void OnUpgrade(DefenseStat stat, int level)
    {
        switch (level)
        {
            case 1:
                stat._speedAttack *= 0.75f;
                break;
            case 2:
                stat._speedAttack *= 0.5f;
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }
}
