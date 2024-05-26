// Marcel
public class AntiArmorStats : CharacterStats
{
    public AntiArmorStats()
    {
        maxHealth = 200f;
        health = maxHealth;
        damagePerSecond = 30f;
        attackSpeed = 70f;
        blockPerSecondMovementSpeed = 0.8f;
        range = 5f;
        deploymentCost = 500f;
        deploymentTime = 300f;
        deathExperience = 100f;
    }
}