public class SupportUpgrade3 : SupportUpgrade
{
    public SupportUpgrade3()
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