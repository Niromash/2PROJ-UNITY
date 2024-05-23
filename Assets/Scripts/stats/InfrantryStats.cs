public class InfantryStats : CharacterStats
{
    public InfantryStats()
    {
        maxHealth = 500f;
        health = maxHealth;
        damagePerSecond = 50f;
        attackSpeed = 100f;
        blockPerSecondMovementSpeed = 20f;
        range = 2f;
        deploymentCost = 150f;
        deploymentTime = 200f;
        deathExperience = 100f;
    }
}