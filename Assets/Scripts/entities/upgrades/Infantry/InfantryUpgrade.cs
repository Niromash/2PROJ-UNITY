public class InfantryUpgrade : UnitUpgrade
{
    public InfantryUpgrade()
    {
        upgradeCost = 100;
    }

    public override EntityTypes GetName()
    {
        return EntityTypes.Infantry;
    }
}