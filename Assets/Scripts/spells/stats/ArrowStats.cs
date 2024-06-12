public class ArrowStats : SpellStats
{
    public ArrowStats()
    {
        damage = 15f;
        spellCount = 30;
        wavesCount = 4;
        cooldown = 3 * 1000f; // in millis
        deploymentCost = 600;
    }

    public override string GetName()
    {
        return "Arrow";
    }
}