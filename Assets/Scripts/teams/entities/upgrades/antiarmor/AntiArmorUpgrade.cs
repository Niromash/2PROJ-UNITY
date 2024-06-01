public class AntiArmorUpgrade : UnitUpgrade
{
    public AntiArmorUpgrade()
    {
        entityMultipliers = new EntityMultipliers();
        upgradeCost = 100;
    }

    public override EntityTypes GetName()
    {
        return EntityTypes.AntiArmor;
    }
}