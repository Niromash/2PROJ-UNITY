using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team
{
    private readonly Side side;
    private int gold;
    private int experience;
    private readonly List<Entity> entities;
    private readonly Tower tower;
    private readonly List<Turret> turrets;

    public int Gold
    {
        get { return gold; }
        set { gold = value; }
    }

    public int Experience
    {
        get { return experience; }
        set { experience = value; }
    }

    public Team(Side side, GameObject towerGameObject, GameManager gameManager)
    {
        this.side = side;
        tower = new Tower(500, towerGameObject, this, gameManager);
        entities = new List<Entity>();
        turrets = new List<Turret>();
    }

    public void AddGold(int amount)
    {
        gold += amount;
        Debug.Log("Gain de" + amount + "gold");
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        Debug.Log("Gain de " + amount + " d'experience!");
    }

    public void AddEntity(Entity entity)
    {
        entities.Add(entity);
    }

    public void RemoveEntity(Entity entity)
    {
        entities.Remove(entity);
    }

    public List<Entity> GetEntities()
    {
        return entities;
    }

    public void AddTurret(Turret turret)
    {
        turrets.Add(turret);
    }

    public void RemoveTurret(Turret turret)
    {
        turrets.Remove(turret);
    }

    public List<Turret> GetTurrets()
    {
        return turrets;
    }

    public Side GetSide()
    {
        return side;
    }

    public Tower GetTower()
    {
        return tower;
    }
}