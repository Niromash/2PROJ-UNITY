﻿using System;

public abstract class Age : EntityStatsMultipliable, TurretStatsMultipliable
{
    protected float goldMultiplier = 1;
    protected float additionalIncomeMultiplier = 1;
    protected float experienceMultiplier = 1;
    protected EntityMultipliers entitiesMultiplier = new EntityMultipliers();
    protected TurretMultipliers turretsMultiplier = new TurretMultipliers();

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
    
    public float GetAdditionalIncomeMultiplier()
    {
        return additionalIncomeMultiplier;
    }

    public EntityMultipliers GetEntitiesStatsMultiplier()
    {
        return entitiesMultiplier;
    }

    public TurretMultipliers GetTurretsStatsMultiplier()
    {
        return turretsMultiplier;
    }
}