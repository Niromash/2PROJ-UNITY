using System;

public class MedievalAge : Age
{
    public MedievalAge()
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
        return "Medieval";
    }

    public override string GetBackgroundAssetName()
    {
        return "medieval_background";
    }

    public override Type GetSpellType()
    {
        return typeof(Arrow);
    }

    public override SpellStats GetSpellStats()
    {
        return new ArrowStats();
    }

    public override int GetAgeEvolvingCost()
    {
        return 70;
    }

    public override int GetAgeLevel()
    {
        return 2;
    }
}