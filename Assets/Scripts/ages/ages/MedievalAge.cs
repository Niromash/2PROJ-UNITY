public class MedievalAge : Age
{
    public MedievalAge()
    {
        incomeMultiplier = 2f;
    }
    
    public override string GetName()
    {
        return "Medieval";
    }    
    
    public override string GetBackgroundAssetName()
    {
        return "medieval_background";
    }
}