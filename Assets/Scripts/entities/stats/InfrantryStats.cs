// Franki tanki
public class InfantryStats : CharacterStats
{
    public InfantryStats()
    {
        maxHealth = 200f;
        health = maxHealth;
        damagePerSecond = 50f;
        blockPerSecondMovementSpeed = 0.2f;
        range = 2f;
        deploymentCost = 150;
        deploymentTime = 1000f;
        deathExperience = 100;
        deathGold = 100;
    }

    public override string GetName()
    {
        return "Infantry";
    }
}