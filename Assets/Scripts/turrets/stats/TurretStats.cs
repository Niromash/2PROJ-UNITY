public abstract class TurretStats : DamagerStats, Nameable
{
    public float damagePerSecond = 100f;
    public float range = 20f;
    public int deploymentCost = 100;
    public float bulletSpeed = 10f;

    public abstract string GetName();

    public float GetDamage()
    {
        return damagePerSecond;
    }

    public void ApplyMultiplier(AgeMultipliers multipliers)
    {
        TurretMultipliers turretsStatsMultiplier = multipliers.GetTurretsStatsMultiplier();
        damagePerSecond *= turretsStatsMultiplier.damagePerSecond;
        range *= turretsStatsMultiplier.range;
        deploymentCost *= (int)(deploymentCost * multipliers.GetGoldMultiplier());
        bulletSpeed *= turretsStatsMultiplier.bulletSpeed;
    }
}