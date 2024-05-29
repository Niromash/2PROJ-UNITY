using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Turret
{
    private GameObject gameObject;
    private GameManager gameManager;
    private Team team;
    private readonly TurretStats stats;
    private List<Turret> turrets;

    public Turret(GameObject go, TurretStats stats, Team team)
    {
        gameObject = go;
        this.stats = stats;
        this.team = team;
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
    
    
}