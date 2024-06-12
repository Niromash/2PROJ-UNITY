public class MeteorStats : SpellStats
{
    public MeteorStats()
    {
        damage = 60f;
        spellCount = 30;
        wavesCount = 2;
        cooldown = 6 * 1000f; // in millis
        deploymentCost = 500;
    }
    
    public override string GetName()
    {
        return "Meteor";
    }
}