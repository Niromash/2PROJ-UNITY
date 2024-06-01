public abstract class SpellStats : DamagerStats, Nameable
{
    public float damage = 100f;
    public int spellCount = 10;
    public int wavesCount = 1;
    public float cooldown = 3 * 1000f; // in millis
    public int deploymentCost = 100; // cost in xp

    public abstract string GetName();

    public float GetDamage()
    {
        return damage;
    }
}