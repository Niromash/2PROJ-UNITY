using System;

public class PrehistoricAge : Age
{
    public PrehistoricAge()
    {
        goldMultiplier = 1f;
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
}