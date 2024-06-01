public class TurretUpgrade2 : TurretUpgrade
{
    public TurretUpgrade2()
    {
        upgradeLevel = 2;

        turretMultipliers = new TurretMultipliers
        {
            damagePerSecond = 2.5f,
            range = 2f,
            bulletSpeed = 3f,
        };
        upgradeCost = 600;
    }
}