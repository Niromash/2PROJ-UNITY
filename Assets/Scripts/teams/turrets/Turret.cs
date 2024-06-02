using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : Damager
{
    private GameObject gameObject;
    private GameManager gameManager;
    private Team team;
    private readonly TurretStats baseStats;
    private TurretStats stats;
    private int upgradeCount;
    private List<Turret> turrets;
    private Age age;
    private readonly int index;

    public Turret(GameObject go, TurretStats stats, Team team, int index)
    {
        gameObject = go;
        baseStats = stats;
        this.stats = (TurretStats)stats.Clone();
        this.team = team;
        this.index = index;
        if (gameObject.activeSelf)
        {
            // If the turret is active (bought), we apply the multiplier
            age = team.GetCurrentAge();
            this.stats.ApplyMultiplier(age);
        }
    }

    public bool CanUpgrade()
    {
        // We can upgrade turrets only if the 3 turrets are active, the team age is the same as the turret age
        return upgradeCount < 3 && age == team.GetCurrentAge() && team.GetUpgradeTurrets().CanUpgradeTurret(index);
    }

    public void Upgrade()
    {
        if (!CanUpgrade())
        {
            Debug.LogWarning("Turret cannot be upgraded");
            return;
        }

        team.GetUpgradeTurrets().UpgradeTurret(index);
        upgradeCount++;

        Debug.Log("Turret upgraded!");
        var changeSprite = gameObject.GetComponent<ChangeSprite>();
        if (changeSprite != null)
        {
            changeSprite.ChangeSpriteToNextLevel();
        }

        stats.ApplyMultiplier(team.GetUpgradeTurrets().GetTurretUpgrade(index));
    }

    public void MakeActive(Age age)
    {
        this.age = age;
        stats.ApplyMultiplier(age);
    }

    public Team GetTeam()
    {
        return team;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }


    public TurretStats GetStats()
    {
        return stats;
    }

    public string GetName()
    {
        return stats.GetName();
    }

    public DamagerStats GetDamagerStats()
    {
        return stats;
    }

    public float GetLevel()
    {
        TurretUpgrade turretUpgrade = team.GetUpgradeTurrets().GetTurretUpgrade(index);
        return turretUpgrade != null ? turretUpgrade.GetUpgradeLevel() : 0;
    }

    public void ResetLevel()
    {
        team.GetUpgradeTurrets().ResetTurretUpgrade(index);
        upgradeCount = 0;
    }

    public void ResetSprite()
    {
        gameObject.GetComponent<ChangeSprite>().ChangeToFirstTurret();
    }

    public void ResetStats()
    {
        stats = (TurretStats)baseStats.Clone();
    }
}