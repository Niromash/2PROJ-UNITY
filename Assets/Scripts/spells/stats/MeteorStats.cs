public class MeteorStats : SpellStats
{
    public MeteorStats()
    {
        damage = 60f;
        spellCount = 20;
        wavesCount = 2;
        cooldown = 200f;
        deploymentCost = 100f;
    }
    
    public override string GetName()
    {
        return "Meteor";
    }
}