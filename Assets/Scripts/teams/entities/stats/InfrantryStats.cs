public class InfantryStats : CharacterStats
{
    public InfantryStats()
    {
        maxHealth = 200f;
        health = maxHealth;
        damagePerSecond = 40f;
        blockPerSecondMovementSpeed = 0.3f;
        range = 2f;
        deploymentCost = 100;
        deploymentTime = 500f;
        deathExperience = 80;
        deathGold = 50;
    }

    public override EntityTypes GetEntityType()
    {
        return EntityTypes.Infantry;
    }
}