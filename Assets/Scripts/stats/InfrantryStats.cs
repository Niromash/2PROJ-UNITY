public class InfantryStats : CharacterStats
{
    public InfantryStats()
    {
        maxHealth = 200f;
        health = maxHealth;
        damagePerSecond = 50f;
        attackSpeed = 100f;
        blockPerSecondMovementSpeed = 1f;
        range = 3f;
        deploymentCost = 150f;
        deploymentTime = 200f;
        deathExperience = 100f;
    }
}