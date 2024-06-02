public class ExtraEntityStats : CharacterStats
{
    public ExtraEntityStats()
    {
        maxHealth = 2000f;
        health = maxHealth;
        damagePerSecond = 300f;
        blockPerSecondMovementSpeed = 0.8f;
        range = 3f;
        deploymentCost = 750;
        deploymentTime = 500f;
        deathExperience = 100;
        deathGold = 50;
    }

    public override EntityTypes GetEntityType()
    {
        return EntityTypes.Extra;
    }
}