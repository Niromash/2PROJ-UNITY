public class SupportStats : CharacterStats
{
    public SupportStats()
    {
        maxHealth = 180f;
        health = maxHealth;
        damagePerSecond = 20f;
        blockPerSecondMovementSpeed = 0.4f;
        range = 6f;
        deploymentCost = 200;
        deploymentTime = 400f;
        deathExperience = 90;
        deathGold = 60;
    }

    public override EntityTypes GetEntityType()
    {
        return EntityTypes.Support;
    }
}