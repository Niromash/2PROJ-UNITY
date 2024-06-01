// Marcel
public class SupportStats : CharacterStats
{
    public SupportStats()
    {
        maxHealth = 200f;
        health = maxHealth;
        damagePerSecond = 30f;
        blockPerSecondMovementSpeed = 0.8f;
        range = 5f;
        deploymentCost = 500;
        deploymentTime = 300f;
        deathExperience = 100;
    }
    
    public override string GetName()
    {
        return "Support";
    }
}