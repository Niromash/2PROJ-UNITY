using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team
{
    private readonly Side side;
    private readonly List<Entity> entities;
    private readonly Tower tower;
    private readonly List<Turret> turrets;
    private readonly Queue<EntityToSpawn> entitiesToSpawn;
    private int gold;
    private int experience;

    public Team(Side side, GameObject towerGameObject, GameManager gameManager)
    {
        this.side = side;
        tower = new Tower(500, towerGameObject, this, gameManager);
        entities = new List<Entity>();
        turrets = new List<Turret>();
        entitiesToSpawn = new Queue<EntityToSpawn>();
        gold = 500;
        experience = 0;
    }

    public int GetGold()
    {
        return gold;
    }

    public int GetExperience()
    {
        return experience;
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

    public void RemoveGold(int amount)
    {
        gold -= amount;
        Debug.Log("Perte de " + amount + " gold");
    }

    public void AddEntity(GameObject prefab, CharacterStats stats, Vector3 spawnPosition)
    {
        entitiesToSpawn.Enqueue(new EntityToSpawn(prefab, this, stats, spawnPosition));
    }

    public void RemoveEntity(Entity entity)
    {
        entities.Remove(entity);
        Object.Destroy(entity.GetGameObject());
    }

    public List<Entity> GetEntities()
    {
        return entities;
    }

    public Queue<EntityToSpawn> GetEntitiesToSpawnQueue()
    {
        return entitiesToSpawn;
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

    public IEnumerator SpawnEntities(GameManager gameManager)
    {
        while (GameManager.GetGameState().Equals(GameState.Playing))
        {
            if (entitiesToSpawn.Count > 0)
            {
                EntityToSpawn entityToSpawn = entitiesToSpawn.Dequeue();
                GameObject spawnedObject = Object.Instantiate(entityToSpawn.GetPrefab(),
                    entityToSpawn.GetSpawnPosition(), Quaternion.identity);
                spawnedObject.SetActive(true);
                spawnedObject.tag = "Untagged";

                if (GetSide().Equals(Side.Enemy)) spawnedObject.GetComponent<SpriteRenderer>().flipX = true;

                entities.Add(new Entity(spawnedObject, this, entityToSpawn.GetStats(), gameManager));

                // get the spawn delay from the stats of the next entity to spawn without removing it from the queue
                float spawnDelay = entitiesToSpawn.Count > 0
                    ? entitiesToSpawn.Peek().GetStats().deploymentTime / 1000
                    : 0;

                yield return new WaitForSeconds(spawnDelay); // the deployment time of the next entity to spawn
            }

            yield return new WaitForSeconds(1f);
        }
    }

    public IEnumerator GainPassiveGolds()
    {
        while (GameManager.GetGameState().Equals(GameState.Playing))
        {
            AddGold(10);
            yield return new WaitForSeconds(1);
        }
    }
}