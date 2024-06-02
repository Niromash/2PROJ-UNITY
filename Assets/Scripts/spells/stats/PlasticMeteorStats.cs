public class PlasticMeteorStats : SpellStats
{
    public PlasticMeteorStats()
    {
        damage = 100f;
        spellCount = 150;
        wavesCount = 2;
        cooldown = 6 * 1000f; // in millis
        deploymentCost = 100;
    }
    
    public override string GetName()
    {
        return "PlasticMeteor";
    }
}