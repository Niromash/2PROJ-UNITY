public class AntiArmorStats : CharacterStats
{
    public AntiArmorStats()
    {
        maxHealth = 200f;
        health = maxHealth;
        damagePerSecond = 30f;
        blockPerSecondMovementSpeed = 0.8f;
        range = 5f;
        deploymentCost = 50;
        deploymentTime = 500f;
        deathExperience = 100;
        deathGold = 50;
    }

    public override string GetName()
    {
        return "AntiArmor";
    }
}