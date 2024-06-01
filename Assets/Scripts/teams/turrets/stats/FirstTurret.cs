public class FirstTurret : TurretStats
{
    public FirstTurret()
    {
        damagePerSecond = 50f;
        range = 12f;
        deploymentCost = 500;
        bulletSpeed = 8f;
    }

    public override string GetName()
    {
        return "First Turret";
    }
}