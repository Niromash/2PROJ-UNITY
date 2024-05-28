using System.Collections.Generic;
using UnityEngine;

public class Turret
{
    private GameObject gameObject;
    private GameManager gameManager;
    private readonly TurretStats stats;
    private List<Turret> turrets;
    private Side side;

    public Turret(GameObject go, TurretStats stats, Side side)
    {
        gameObject = go;
        this.side = side;
        this.stats = stats;
    }
    
    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public Side GetSide()
    {
        return side;
    }
    
    public TurretStats GetStats()
    {
        return stats;
    }
}