public abstract class SpellStats : DamagerStats
{
    public string name = "Spell";
    public float damage = 100f;
    public int spellCount = 10;
    public int wavesCount = 1;
    public float cooldown = 100f;
    public float deploymentCost = 100f;
    public float deploymentTime = 100f;
    public float deathExperience = 100f;

    public float GetDamage()
    {
        return damage;
    }
}