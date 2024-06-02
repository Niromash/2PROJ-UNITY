using System;
using UnityEngine;

public class TurretStats : DamagerStats, Nameable, ICloneable
{
    public float damagePerSecond = 50f;
    public float range = 12f;
    public int deploymentCost = 500;
    public float bulletSpeed = 8f;

    public string GetName()
    {
        return "Turret";
    }

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

    public object Clone()
    {
        return MemberwiseClone();
    }
}