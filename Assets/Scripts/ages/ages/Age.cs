using System;

public abstract class Age
{
    public float incomeMultiplier = 1;
    public float entitiesStatsMultiplier = 1;
    public float turretsStatsMultiplier = 1;
    public abstract string GetName();
    public abstract string GetBackgroundAssetName();
    public abstract Type GetSpellType();
    public abstract SpellStats GetSpellStats();
}