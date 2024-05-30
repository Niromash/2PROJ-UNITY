using System;

public class PrehistoricAge : Age
{
    public PrehistoricAge()
    {
        goldMultiplier = 1f;
        experienceMultiplier = 1f;
        entitiesStatsMultiplier = 2f;
        turretsStatsMultiplier = 1f;
    }

    public override string GetName()
    {
        return "Prehistoric";
    }
    
    public override string GetBackgroundAssetName()
    {
        return "prehistoric_background";
    }

    public override Type GetSpellType()
    {
        return typeof(Meteor);
    }

    public override SpellStats GetSpellStats()
    {
        return new MeteorStats();
    }

    public override int GetAgeEvolvingCost()
    {
        return 0;
    }
}