using System;

public abstract class Age : AgeMultipliers
{
    protected float goldMultiplier = 1;
    protected float experienceMultiplier = 1;
    protected AgeMultipliers.Entities entitiesMultiplier = new AgeMultipliers.Entities();
    protected AgeMultipliers.Turrets turretsMultiplier = new AgeMultipliers.Turrets();

    public abstract string GetName();
    public abstract string GetBackgroundAssetName();
    public abstract Type GetSpellType();
    public abstract SpellStats GetSpellStats();
    public abstract int GetAgeEvolvingCost();
    public abstract int GetAgeLevel();

    public float GetGoldMultiplier()
    {
        return goldMultiplier;
    }

    public float GetExperienceMultiplier()
    {
        return experienceMultiplier;
    }

    public AgeMultipliers.Entities GetEntitiesStatsMultiplier()
    {
        return entitiesMultiplier;
    }

    public AgeMultipliers.Turrets GetTurretsStatsMultiplier()
    {
        return turretsMultiplier;
    }
}