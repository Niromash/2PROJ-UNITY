public abstract class UnitUpgrade : EntityMultipliable
{
    protected int upgradeCost;
    protected EntityMultipliers entityMultipliers;
    protected float goldMultiplier = 1f;
    protected float experienceMultiplier = 1f;
    protected int upgradeLevel = 1;

    public abstract EntityTypes GetName();

    public int GetUpgradeCost()
    {
        return upgradeCost;
    }

    public EntityMultipliers GetEntitiesStatsMultiplier()
    {
        return entityMultipliers;
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