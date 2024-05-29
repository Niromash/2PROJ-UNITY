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

    public Turret(GameObject go, TurretStats stats, Team team)
    {
        gameObject = go;
        this.stats = stats;
        this.team = team;
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
        return stats.name;
    }

    public Team GetTeam()
    {
        return team;
    }

    public DamagerStats GetDamagerStats()
    {
        return stats;
    }
}