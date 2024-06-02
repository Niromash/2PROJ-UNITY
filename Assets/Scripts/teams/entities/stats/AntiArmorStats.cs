public class AntiArmorStats : CharacterStats
{
    public AntiArmorStats()
    {
        maxHealth = 250f;
        health = maxHealth;
        damagePerSecond = 60f;
        blockPerSecondMovementSpeed = 0.3f;
        range = 2f;
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