public abstract class UnitUpgrade : UnitUpgradeMultipliers
{
    protected float TankStatsMultiplier = 1.15f;
    protected float SupportStatsMultiplier = 1.15f;
    protected float InfantryStatsMultiplier = 1.15f;
    protected float AntiArmorStatsMultiplier = 1.15f;
    protected int upgradeCost;
    public abstract string GetName();
    public abstract string GetUnitType();
    public abstract int GetUpgradeCost();

    public float GetTankStatsMultiplier()
    {
        return TankStatsMultiplier;
    }

    public float GetSupportStatsMultiplier()
    {
        return SupportStatsMultiplier;
    }

    public float GetInfantryStatsMultiplier()
    {
        return InfantryStatsMultiplier;
    }
    
    public float GetAntiArmorStatsMultiplier()
    {
        return AntiArmorStatsMultiplier;
    }
}