using UnityEngine;

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

    public void ApplyMultiplier(TurretStatsMultipliable multipliers)
    {
        TurretMultipliers turretsStatsMultiplier = multipliers.GetTurretsStatsMultiplier();
        damagePerSecond *= turretsStatsMultiplier.damagePerSecond;
        range *= turretsStatsMultiplier.range;
        
        deploymentCost = (int)(deploymentCost * multipliers.GetGoldMultiplier());
        bulletSpeed *= turretsStatsMultiplier.bulletSpeed;
    }

    public TurretStats GetMultipliedStats(TurretStatsMultipliable multipliers)
    {
        TurretStats multipliedStats = (TurretStats)MemberwiseClone();
        multipliedStats.ApplyMultiplier(multipliers);
        return multipliedStats;
    }
}