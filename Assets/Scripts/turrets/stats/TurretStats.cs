public abstract class TurretStats : DamagerStats
{
    public float health = 100f;
    public float damagePerSecond = 100f;
    public float attackSpeed = 100f;
    public float range = 20f;
    public float deploymentCost = 100f;
    public string name = "Turret";
    public float GetDamage()
    {
        return damagePerSecond;
    }
}