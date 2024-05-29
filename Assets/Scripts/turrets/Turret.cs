using System.Collections.Generic;
using UnityEngine;

public class Turret : Damager
{
    private GameObject gameObject;
    private GameManager gameManager;
    private readonly TurretStats stats;
    private List<Turret> turrets;
    private Team team;

    public Turret(GameObject go, TurretStats stats, Team team)
    {
        gameObject = go;
        this.stats = stats;
        this.team = team;
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