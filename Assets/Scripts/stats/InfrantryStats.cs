public class InfantryStats : CharacterStats
{
    public InfantryStats()
    {
        maxHealth = 200f;
        health = maxHealth;
        damagePerSecond = 50f;
        attackSpeed = 100f;
        blockPerSecondMovementSpeed = 0.8f;
        range = 100f;
        deploymentCost = 150f;
        deploymentTime = 200f;
        deathExperience = 100f;
    }
}