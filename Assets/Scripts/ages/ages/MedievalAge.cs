using System;

public class MedievalAge : Age
{
    public MedievalAge()
    {
        incomeMultiplier = 2f;
        entitiesStatsMultiplier = 3f;
        turretsStatsMultiplier = 1.5f;
    }

    public override string GetName()
    {
        return "Medieval";
    }

    public override string GetBackgroundAssetName()
    {
        return "medieval_background";
    }

    public override Type GetSpellType()
    {
        return typeof(Arrow);
    }

    public override SpellStats GetSpellStats()
    {
        return new ArrowStats();
    }
}