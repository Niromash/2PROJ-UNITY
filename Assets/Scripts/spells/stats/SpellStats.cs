public abstract class SpellStats : DamagerStats, Nameable
{
    public float damage = 100f;
    public int spellCount = 10;
    public int wavesCount = 1;
    public float cooldown = 100f;
    public float deploymentCost = 100f; // cost in xp

    public abstract string GetName();

    public float GetDamage()
    {
        return damage;
    }
}