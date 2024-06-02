public class InfantryUpgrade1 : InfantryUpgrade
{
    public InfantryUpgrade1()
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
        upgradeCost = 1000;
    }
}