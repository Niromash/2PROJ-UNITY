public class InfantryUpgrade : UnitUpgrade
{
    public InfantryUpgrade()
    {
        upgradeCost = 100;
    }

    public override string GetName()
    {
        return "Infantry Upgrade";
    }

    public override string GetUnitType()
    {
        return "Infantry";
    }

    public override int GetUpgradeCost()
    {
        return upgradeCost;
    }
}