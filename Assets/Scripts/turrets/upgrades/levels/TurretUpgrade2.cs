public class TurretUpgrade2 : TurretUpgrade
{
    public TurretUpgrade2()
    {
        upgradeLevel = 2;

        turretMultipliers = new TurretMultipliers
        {
            damagePerSecond = 4f,
            range = 3f,
            bulletSpeed = 3f,
        };
        upgradeCost = 600;
    }
}