public class SupportUpgrade2 : SupportUpgrade
{
    public SupportUpgrade2()
    {
        upgradeLevel = 2;

        entityMultipliers = new EntityMultipliers
        {
            damagePerSecond = 3f,
            maxHealth = 6f,
            range = 1f,
            blockPerSecondMovementSpeed = 1f,
            deploymentTime = 1f,
        };
        upgradeCost = 300;
    }
}