using System.Collections.Generic;
using UnityEngine;

public class Turret : Damager
{
    private GameObject gameObject;
    private GameManager gameManager;
    private Team team;
    private readonly TurretStats stats;
    private int upgradeCount = 0;
    private List<Turret> turrets;
    private readonly Age age;

    public Turret(GameObject go, TurretStats stats, Team team)
    {
        gameObject = go;
        this.stats = stats;
        this.team = team;
        age = team.GetCurrentAge();
        stats.ApplyMultiplier(age.turretsStatsMultiplier);
    }

    public bool CanUpgrade()
    {
        return upgradeCount < 3;
    }

    public void Upgrade(float factor)
    {
        if (!CanUpgrade())
        {
            Debug.LogWarning("Turret has reached maximum upgrades.");
            return;
        }

        upgradeCount++;
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
}