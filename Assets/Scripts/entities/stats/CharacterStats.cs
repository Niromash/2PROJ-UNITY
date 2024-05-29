public abstract class CharacterStats : DamagerStats
{
    public string name = "Character";
    public float maxHealth = 100f;
    public float health = 100f;
    public float damagePerSecond = 100f;
    public float attackSpeed = 100f;
    public float blockPerSecondMovementSpeed = 1f;
    public float range = 2f;
    public int deploymentCost = 100;
    public float deploymentTime = 1000f;
    public int deathExperience = 100;
    public int deathGold = 100;

    public float GetDamage()
    {
        return damagePerSecond;
    }
}