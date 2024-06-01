public abstract class TurretUpgrade : TurretStatsMultipliable
{
    protected int upgradeCost;
    protected TurretMultipliers turretMultipliers;
    protected float goldMultiplier = 1f;
    protected float experienceMultiplier = 1f;
    protected int upgradeLevel = 1;
    
    public TurretUpgrade()
    {
        turretMultipliers = new TurretMultipliers();
        upgradeCost = 100;
    }

    public int GetUpgradeCost()
    {
        return upgradeCost;
    }

    public TurretMultipliers GetTurretsStatsMultiplier()
    {
        return turretMultipliers;
    }

    public float GetGoldMultiplier()
    {
        return goldMultiplier;
    }

    public float GetExperienceMultiplier()
    {
        return experienceMultiplier;
    }

    public int GetUpgradeLevel()
    {
        return upgradeLevel;
    }
}