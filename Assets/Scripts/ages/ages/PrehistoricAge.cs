using System;
using System.Collections.Generic;
using UnityEngine;

public class PrehistoricAge : Age
{
    public PrehistoricAge()
    {
        goldMultiplier = 1f;
        additionalIncomeMultiplier = 1f;
        experienceMultiplier = 1f;
        entitiesMultiplier = new EntityMultipliers
        {
            damagePerSecond = 2f,
            maxHealth = 1.5f,
            range = 1.2f,
            deploymentTime = 1,
            blockPerSecondMovementSpeed = 1.5f
        };
        turretsMultiplier = new TurretMultipliers
        {
            damagePerSecond = 1.5f,
            range = 1.2f,
            bulletSpeed = 1.5f,
        };
    }

    public override string GetName()
    {
        return "Prehistoric";
    }

    public override string GetBackgroundAssetName()
    {
        return "prehistoric_background";
    }

    public override Type GetSpellType()
    {
        return typeof(Meteor);
    }

    public override SpellStats GetSpellStats()
    {
        return new MeteorStats();
    }

    public override int GetAgeEvolvingCost()
    {
        return 0;
    }

    public override int GetAgeLevel()
    {
        return 1;
    }

    public override List<Vector3> GetTurretsPositions()
    {
        Vector3 turretPosition1 = new Vector3(-11.5f, 5, 0);
        Vector3 turretPosition2 = new Vector3(-12.5f, 6, 0);
        Vector3 turretPosition3 = new Vector3(-13.5f, 7, 0);
        Vector3 turretPosition4 = new Vector3(-14.5f, 8, 0);

        return new List<Vector3> { turretPosition1, turretPosition2, turretPosition3, turretPosition4 };
    }
}