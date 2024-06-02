public class TankStats : CharacterStats
{
    public TankStats()
    {
        maxHealth = 500f;
        health = maxHealth;
        damagePerSecond = 25f;
        blockPerSecondMovementSpeed = 0.1f;
        range = 2f;
        deploymentCost = 300;
        deploymentTime = 800f;
        deathExperience = 150;
        deathGold = 150;
    }

    public override EntityTypes GetEntityType()
    {
        return EntityTypes.Tank;
    }
}