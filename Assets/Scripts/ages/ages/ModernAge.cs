using System;
using System.Collections.Generic;
using UnityEngine;

public class ModernAge : Age
{
    public ModernAge()
    {
        goldMultiplier = 1.2f;
        additionalIncomeMultiplier = 5f;
        experienceMultiplier = 2f;
        entitiesMultiplier = new EntityMultipliers
        {
            damagePerSecond = 3f,
            maxHealth = 3f,
            range = 2f,
            deploymentTime = .8f,
            blockPerSecondMovementSpeed = 1.7f
        };
        turretsMultiplier = new TurretMultipliers
        {
            damagePerSecond = 2f,
            range = 1.2f,
            bulletSpeed = 2f,
        };
    }

    public override string GetName()
    {
        return "Modern";
    }

    public override string GetBackgroundAssetName()
    {
        return "modern_background";
    }

    public override Type GetSpellType()
    {
        return typeof(PlasticMeteor);
    }

    public override SpellStats GetSpellStats()
    {
        return new PlasticMeteorStats();
    }

    public override int GetAgeEvolvingCost()
    {
        return 400;
    }

    public override int GetAgeLevel()
    {
        return 2;
    }

    public override List<Vector3> GetTurretsPositions()
    {
        Vector3 turretPosition1 = new Vector3(-15.5f, 8, 0);
        Vector3 turretPosition2 = new Vector3(-16.5f, 8, 0);
        Vector3 turretPosition3 = new Vector3(-17.5f, 8, 0);
        Vector3 turretPosition4 = new Vector3(-18.5f, 8, 0);

        return new List<Vector3> { turretPosition1, turretPosition2, turretPosition3, turretPosition4 };
    }
    
    public override List<Vector3> GetTurretsPositionsOfEnnemy()
    {
        Vector3 turretPosition1 = new Vector3(31.5f, 8, 0);
        Vector3 turretPosition2 = new Vector3(32.5f, 8, 0);
        Vector3 turretPosition3 = new Vector3(33.5f, 8, 0);
        Vector3 turretPosition4 = new Vector3(34.5f, 8, 0);

        return new List<Vector3> { turretPosition1, turretPosition2, turretPosition3, turretPosition4 };
    }
}