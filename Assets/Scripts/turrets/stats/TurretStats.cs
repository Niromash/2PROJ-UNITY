public abstract class TurretStats
{
    public float damagePerSecond = 100f;
    public float range = 20f;
    public float deploymentCost = 100f;
    public float bulletSpeed = 10f;
    
    public void Upgrade(float upgradeFactor)
    {
        damagePerSecond *= upgradeFactor;
    }
}