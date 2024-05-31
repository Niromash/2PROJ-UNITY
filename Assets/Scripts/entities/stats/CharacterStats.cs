using UnityEngine;

public abstract class CharacterStats : DamagerStats
{
    public string name = "Character";
    public float maxHealth = 100f;
    public float health = 100f;
    public float damagePerSecond = 100f;
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

    public void ApplyMultiplier(AgeMultipliers multipliers)
    {
        maxHealth *= multipliers.GetEntitiesStatsMultiplier();
        health *= multipliers.GetEntitiesStatsMultiplier();
        damagePerSecond *= multipliers.GetEntitiesStatsMultiplier();
        deploymentCost = (int)(deploymentCost * multipliers.GetGoldMultiplier());
        deathExperience = (int)(deathExperience * multipliers.GetExperienceMultiplier());
        deathGold = (int)(deathGold * multipliers.GetGoldMultiplier());
    }

    public CharacterStats GetMultipliedStats(Age age)
    {
        CharacterStats stats = (CharacterStats)MemberwiseClone();
        stats.ApplyMultiplier(age);
        return stats;
    }
}