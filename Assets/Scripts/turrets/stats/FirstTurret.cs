﻿public class FirstTurret : TurretStats
{
    public FirstTurret()
    {
        damagePerSecond = 30f;
        range = 6f;
        deploymentCost = 500;
        bulletSpeed = 10f;
    }

    public override string GetName()
    {
        return "First Turret";
    }
}