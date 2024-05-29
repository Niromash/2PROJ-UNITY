public class PrehistoricAge : Age
{
    public PrehistoricAge()
    {
        incomeMultiplier = 1.5f;
    }

    public override string GetName()
    {
        return "Prehistoric";
    }
    
    public override string GetBackgroundAssetName()
    {
        return "prehistoric_background";
    }
}