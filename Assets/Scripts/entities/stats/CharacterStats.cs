public abstract class CharacterStats : DamagerStats, Nameable
{
    public float maxHealth = 100f;
    public float health;
    public float damagePerSecond = 100f;
    public float blockPerSecondMovementSpeed = 1f;
    public float range = 2f;
    public int deploymentCost = 100;
    public float deploymentTime = 1000f;
    public int deathExperience = 100;
    public int deathGold = 100;

    public CharacterStats()
    {
        health = maxHealth;
    }

    public string GetName()
    {
        return GetEntityType().ToString();
    }

    public abstract EntityTypes GetEntityType();

    public float GetDamage()
    {
        return damagePerSecond;
    }

    public void ApplyMultiplier(EntityStatsMultipliable multipliers)
    {
        EntityMultipliers entitiesStatsMultiplier = multipliers.GetEntitiesStatsMultiplier();

        maxHealth *= entitiesStatsMultiplier.maxHealth;
        health = maxHealth; // Re update the health

        damagePerSecond *= entitiesStatsMultiplier.damagePerSecond;
        blockPerSecondMovementSpeed *= entitiesStatsMultiplier.blockPerSecondMovementSpeed;
        range *= entitiesStatsMultiplier.range;
        deploymentTime *= entitiesStatsMultiplier.deploymentTime;

        deploymentCost = (int)(deploymentCost * multipliers.GetGoldMultiplier());
        deathExperience = (int)(deathExperience * multipliers.GetExperienceMultiplier());
        deathGold = (int)(deathGold * multipliers.GetGoldMultiplier());
    }

    public CharacterStats GetMultipliedStats(Team team)
    {
        CharacterStats stats = (CharacterStats)MemberwiseClone();

        stats.ApplyMultiplier(team.GetCurrentAge());
        UnitUpgrade unitUpgrade = team.GetUpgradeUnits().GetUnitUpgrade(GetEntityType());
        if (unitUpgrade != null) stats.ApplyMultiplier(unitUpgrade);

        return stats;
    }
}