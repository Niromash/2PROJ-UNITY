public class TurretUpgrade1 : TurretUpgrade
{
    public TurretUpgrade1()
    {
        upgradeLevel = 1;

        turretMultipliers = new TurretMultipliers
        {
            damagePerSecond = 2f,
            range = 2f,
            bulletSpeed = 1f,
        };
        upgradeCost = 300;
    }
}