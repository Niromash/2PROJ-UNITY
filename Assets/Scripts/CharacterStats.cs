using UnityEngine;

public class CharacterStats
{
    public float Health { get; set; } = 100f;
    public float DamagePerSecond { get; set; } = 100f;
    public float AttackSpeed { get; set; } = 100f;
    public float BlockPerSecondMovementSpeed { get; set; } = 100f;
    public float Range { get; set; } = 100f;
    public float DeploymentCost { get; set; } = 100f;
    public float DeploymentTime { get; set; } = 100f;
    public float DeathExperience { get; set; } = 100f;
}

public class InfantryStats : CharacterStats
{
    public InfantryStats()
    {
        Health = 200f;
        DamagePerSecond = 50f;
        AttackSpeed = 100f;
        BlockPerSecondMovementSpeed = 80f;
        Range = 100f;
        DeploymentCost = 150f;
        DeploymentTime = 200f;
        DeathExperience = 100f;
    }
}

public class AntiArmorStats : CharacterStats
{
    public AntiArmorStats()
    {
        Health = 200f;
        DamagePerSecond = 30f;
        AttackSpeed = 70f;
        BlockPerSecondMovementSpeed = 60f;
        Range = 100f;
        DeploymentCost = 500f;
        DeploymentTime = 300f;
        DeathExperience = 100f;
    }
}