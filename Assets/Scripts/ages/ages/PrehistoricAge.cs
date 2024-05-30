using System;

public class PrehistoricAge : Age
{
    public PrehistoricAge()
    {
        incomeMultiplier = 1.5f;
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
}