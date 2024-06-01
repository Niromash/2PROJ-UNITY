public class SupportUpgrade : UnitUpgrade
{
    public SupportUpgrade()
    {
        upgradeCost = 100;
    }

    public override string GetName()
    {
        return "Support Upgrade";
    }

    public override string GetUnitType()
    {
        return "Support";
    }

    public override int GetUpgradeCost()
    {
        return upgradeCost;
    }
}