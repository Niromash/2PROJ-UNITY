public class TankUpgrade : UnitUpgrade
{
    public TankUpgrade()
    {
        upgradeCost = 100;
    }

    public override EntityTypes GetName()
    {
        return EntityTypes.Tank;
    }
}