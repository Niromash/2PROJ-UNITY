// tanki
public class TankStats : CharacterStats
{
    public TankStats()
    {
        maxHealth = 200f;
        health = maxHealth;
        damagePerSecond = 30f;
        attackSpeed = 70f;
        blockPerSecondMovementSpeed = 0.8f;
        range = 5f;
        deploymentCost = 500;
        deploymentTime = 300f;
        deathExperience = 100;
        deathGold = 100;
        name = "Tanki";
    }
}