public class TurretUpgrade1 : TurretUpgrade
{
    public TurretUpgrade1()
    {
        upgradeLevel = 1;

        turretMultipliers = new TurretMultipliers
        {
            damagePerSecond = 2f,
            range = 1.5f,
            bulletSpeed = 2f,
        };
        upgradeCost = 300;
    }
}