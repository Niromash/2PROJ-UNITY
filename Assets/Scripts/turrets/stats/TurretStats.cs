public abstract class TurretStats : DamagerStats
{
    public float damagePerSecond = 100f;
    public float range = 20f;
    public float deploymentCost = 100f;
    public string name = "Turret";
    public float bulletSpeed = 10f;
    
    public float GetDamage()
    {
        return damagePerSecond;
    }
    
    public void Upgrade(float upgradeFactor)
    {
        damagePerSecond *= upgradeFactor;
    }
}