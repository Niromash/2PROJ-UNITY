public class AntiArmorUpgrade : UnitUpgrade
{
    public AntiArmorUpgrade()
    {
        upgradeCost = 100;
    }

    public override string GetName()
    {
        return "Anti-Armor Upgrade";
    }

    public override string GetUnitType()
    {
        return "Anti-Armor";
    }

    public override int GetUpgradeCost()
    {
        return upgradeCost;
    }
}