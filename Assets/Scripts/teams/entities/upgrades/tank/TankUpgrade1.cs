public class TankUpgrade1 : TankUpgrade
{
    public TankUpgrade1()
    {
        upgradeLevel = 1;

        entityMultipliers = new EntityMultipliers
        {
            damagePerSecond = 2f,
            maxHealth = 2f,
            range = 1f,
            blockPerSecondMovementSpeed = 1f,
            deploymentTime = 1f,
        };
        upgradeCost = 500;
    }
}