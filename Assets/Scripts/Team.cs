﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class Team
{
    private readonly Side side;
    private readonly List<Entity> entities;
    private readonly Tower tower;
    private readonly Queue<EntityToSpawn> entitiesToSpawn;
    private Age currentAge;

    private int gold;
    private int experience;
    private int maxExperience;
    private readonly object goldLock = new object();
    private readonly object experienceLock = new object();

    private Image expBarImage;
    private TextMeshProUGUI goldCountText;
    private TextMeshProUGUI expCountText;
    private int? lockedEntityIndex;

    public Team(Side side, GameObject towerGameObject, GameManager gameManager)
    {
        currentAge = new PrehistoricAge();
        if (side.Equals(Side.Player)) ToggleLockEntityUi(3); // the player has ui
        else ToggleLockEntity(0); // the enemy has no UI

        this.side = side;
        tower = new Tower(500, towerGameObject, this, gameManager);
        entities = new List<Entity>();
        entitiesToSpawn = new Queue<EntityToSpawn>();
        gold = 5000;
        experience = 5000;
        maxExperience = 500; // exemple d'expérience maximale pour remplir la barre
        GameObject expBar = GameObject.Find(side.Equals(Side.Player) ? "TowerLeftExpBar" : "TowerRightExpBar");
        GameObject goldcount = GameObject.Find(side.Equals(Side.Player) ? "GoldLeft" : "GoldRight");
        GameObject expcount = GameObject.Find(side.Equals(Side.Player) ? "ExpLeftText" : "ExpRightText");
        goldCountText = goldcount.GetComponent<TextMeshProUGUI>();
        expCountText = expcount.GetComponent<TextMeshProUGUI>();
        expBarImage = expBar.GetComponent<Image>();

        UpdateExpBar();
        DisplayGold();
        UpdateExpBar();
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
        lock (goldLock)
        {
            gold += amount;
        }

        DisplayGold();
    }

    public void AddExperience(int amount)
    {
        lock (experienceLock)
        {
            experience += amount;
        }

        UpdateExpBar();
    }

    public void RemoveGold(int amount)
    {
        lock (goldLock)
        {
            gold -= amount;
        }

        DisplayGold();
    }

    public void RemoveExperience(int amount)
    {
        lock (goldLock)
        {
            experience -= amount;
        }

        UpdateExpBar();
    }

    public void AddEntity(GameObject prefab, CharacterStats stats, Vector3 spawnPosition, string entityName)
    {
        entitiesToSpawn.Enqueue(new EntityToSpawn(prefab, this, stats, spawnPosition, entityName));
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

    public Side GetSide()
    {
        return side;
    }

    public Tower GetTower()
    {
        return tower;
    }

    public IEnumerator SpawnEntities()
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
                spawnedObject.name = entityToSpawn.GetEntityName();

                entities.Add(new Entity(spawnedObject, this, entityToSpawn.GetStats()));

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
            AddGold(Convert.ToInt32(Math.Round(10 * currentAge.GetGoldMultiplier())));
            yield return new WaitForSeconds(1);
        }
    }

    public void UpdateExpBar()
    {
        if (expBarImage != null)
        {
            float expPercentage = (float)experience / maxExperience;
            expBarImage.color = Color.Lerp(Color.cyan, Color.blue, expPercentage);
            expBarImage.fillAmount = expPercentage;
        }

        if (expCountText != null)
        {
            expCountText.text = experience.ToString();
        }
    }

    public void DisplayGold()
    {
        if (goldCountText != null)
        {
            goldCountText.text = gold.ToString();
        }
    }

    public void SetCurrentAge(Age age)
    {
        currentAge = age;
    }

    public Age GetCurrentAge()
    {
        return currentAge;
    }

    public bool GreaterAgeThan(Team team)
    {
        return currentAge.GetAgeLevel() > team.GetCurrentAge().GetAgeLevel();
    }

    public void ToggleLockEntity(int? entityIndexToToggle)
    {
        if (lockedEntityIndex != null) lockedEntityIndex = null;
        else lockedEntityIndex = entityIndexToToggle;
    }

    public void ToggleLockEntityUi(int? entityIndexToToggle)
    {
        GameObject spawnEntitiesButtons = GameObject.Find("SpawnEntities");
        if (spawnEntitiesButtons == null)
        {
            Debug.LogError("SpawnEntities GameObject not found");
            return;
        }

        if (lockedEntityIndex != null)
        {
            // Remove the lock object if it exists
            GameObject existingLockObject =
                spawnEntitiesButtons.transform.GetChild(lockedEntityIndex.Value).Find("Lock").gameObject;
            if (existingLockObject != null)
            {
                Object.Destroy(existingLockObject);
            }

            ToggleLockEntity(entityIndexToToggle);
        }

        if (entityIndexToToggle == null) return;

        GameObject childToLock = spawnEntitiesButtons.transform.GetChild(entityIndexToToggle.Value).gameObject;

        GameObject lockPrefab = Resources.Load<GameObject>("Common/lock/Lock");
        GameObject lockObject = Object.Instantiate(lockPrefab, childToLock.transform.position, Quaternion.identity);
        lockObject.name = "Lock";
        lockObject.transform.SetParent(childToLock.transform);

        ToggleLockEntity(entityIndexToToggle);
    }

    public int? GetLockedEntityIndex()
    {
        return lockedEntityIndex;
    }
}