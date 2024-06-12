public class AntiArmorStats : CharacterStats
{
    public AntiArmorStats()
    {
        maxHealth = 120f;
        health = maxHealth;
        damagePerSecond = 30f;
        blockPerSecondMovementSpeed = 0.3f;
        range = 3f;
        deploymentCost = 150;
        deploymentTime = 700f;
        deathExperience = 120;
        deathGold = 75;
    }

    public override EntityTypes GetEntityType()
    {
        return EntityTypes.AntiArmor;
    }
}