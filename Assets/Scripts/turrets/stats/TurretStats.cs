public abstract class TurretStats : DamagerStats, Nameable
{
    public float damagePerSecond = 100f;
    public float range = 20f;
    public float deploymentCost = 100f;
    public float bulletSpeed = 10f;

    public abstract string GetName();

    public float GetDamage()
    {
        return damagePerSecond;
    }

    public void ApplyMultiplier(float multiplier)
    {
        damagePerSecond *= multiplier;
    }
}