public class ExtraEntityStats : CharacterStats
{
    public ExtraEntityStats()
    {
        maxHealth = 2000f;
        health = maxHealth;
        damagePerSecond = 240f;
        blockPerSecondMovementSpeed = 0.1f;
        range = 5f;
        deploymentCost = 800;
        deploymentTime = 500f;
        deathExperience = 100;
        deathGold = 50;
    }

    public override EntityTypes GetEntityType()
    {
        return EntityTypes.Extra;
    }
}