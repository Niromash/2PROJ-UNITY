public class TankUpgrade3 : TankUpgrade
{
    public TankUpgrade3()
    {
        upgradeLevel = 3;

        entityMultipliers = new EntityMultipliers
        {
            damagePerSecond = 3.5f,
            maxHealth = 7f,
            range = 1f,
            blockPerSecondMovementSpeed = 1f,
            deploymentTime = 1f,
        };
        upgradeCost = 300;
    }
}