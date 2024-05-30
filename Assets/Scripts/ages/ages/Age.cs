using System;

public abstract class Age : AgeMultipliers
{
    protected float goldMultiplier = 1;
    protected float experienceMultiplier = 1;
    protected float entitiesStatsMultiplier = 1;
    protected float turretsStatsMultiplier = 1;
    
    public abstract string GetName();
    public abstract string GetBackgroundAssetName();
    public abstract Type GetSpellType();
    public abstract SpellStats GetSpellStats();
    public abstract int GetAgeEvolvingCost();

    public float GetGoldMultiplier()
    {
        return goldMultiplier;
    }

    public float GetExperienceMultiplier()
    {
        return experienceMultiplier;
    }
    
    public float GetEntitiesStatsMultiplier()
    {
        return entitiesStatsMultiplier;
    }
    
    public float GetTurretsStatsMultiplier()
    {
        return turretsStatsMultiplier;
    }
}