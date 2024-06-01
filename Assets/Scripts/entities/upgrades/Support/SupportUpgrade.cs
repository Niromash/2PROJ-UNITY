public class SupportUpgrade : UnitUpgrade
{
    public SupportUpgrade()
    {
        upgradeCost = 100;
    }

    public override EntityTypes GetName()
    {
        return EntityTypes.Support;
    }
}