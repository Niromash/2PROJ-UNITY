public class TankUpgrade : UnitUpgrade
{
    public TankUpgrade()
    {
        upgradeCost = 100;
    }

    public override string GetName()
    {
        return "Tank Upgrade";
    }

    public override string GetUnitType()
    {
        return "Tank";
    }

    public override int GetUpgradeCost()
    {
        return upgradeCost;
    }
}