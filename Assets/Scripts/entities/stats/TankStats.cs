// tanki
public class TankStats : CharacterStats
{
    public TankStats()
    {
        name = "Tank";
        maxHealth = 300f;
        health = maxHealth;
        damagePerSecond = 30f;
        blockPerSecondMovementSpeed = 0.8f;
        range = 5f;
        deploymentCost = 500;
        deploymentTime = 300f;
        deathExperience = 100;
        deathGold = 100;
    }
}