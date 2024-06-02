using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

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

    private float teamGoldMultiplier = 1;
    private Image expBarImage;
    private TextMeshProUGUI goldCountText;
    private TextMeshProUGUI expCountText;
    private int? lockedEntityIndex;
    private readonly UpgradeUnits upgradeUnits;
    private readonly UpgradeTurrets upgradeTurrets;
    private readonly GameManager gameManager;
    private float spellCooldowns;

    public Team(Side side, GameObject towerGameObject, GameManager gameManager)
    {
        currentAge = new PrehistoricAge();
        if (side.Equals(Side.Player)) ToggleLockEntityUi(3); // the player has ui
        else ToggleLockEntity(0); // the enemy has no UI

        this.side = side;
        tower = new Tower(2000, towerGameObject, this, gameManager);
        entities = new List<Entity>();
        entitiesToSpawn = new Queue<EntityToSpawn>();
        gold = 200;
        experience = 0;
        maxExperience = 500; // exemple d'expérience maximale pour remplir la barre
        GameObject expBar = GameObject.Find(side.Equals(Side.Player) ? "TowerLeftExpBar" : "TowerRightExpBar");
        GameObject goldcount = GameObject.Find(side.Equals(Side.Player) ? "GoldLeft" : "GoldRight");
        GameObject expcount = GameObject.Find(side.Equals(Side.Player) ? "ExpLeftText" : "ExpRightText");
        goldCountText = goldcount.GetComponent<TextMeshProUGUI>();
        expCountText = expcount.GetComponent<TextMeshProUGUI>();
        expBarImage = expBar.GetComponent<Image>();
        this.gameManager = gameManager;

        upgradeUnits = new UpgradeUnits(this);
        upgradeTurrets = new UpgradeTurrets(this);

        UpdateTurretPosition();
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

    public void UpdateTurretPosition()
    {
        List<Vector3> turretsPositions;
        Team mostAdvancedAgeTeam = gameManager.GetMostAdvancedAgeTeam();
        if (mostAdvancedAgeTeam == null)
        {
            if (side.Equals(Side.Player)) turretsPositions = currentAge.GetTurretsPositions();
            else turretsPositions = currentAge.GetTurretsPositionsOfEnnemy();
        }
        else
        {
            if (side.Equals(Side.Player)) turretsPositions = mostAdvancedAgeTeam.GetCurrentAge().GetTurretsPositions();
            else turretsPositions = mostAdvancedAgeTeam.GetCurrentAge().GetTurretsPositionsOfEnnemy();
        }

        List<Turret> turrets = tower.GetTurrets();
        if (turretsPositions.Count != turrets.Count) return;

        for (int i = 0; i < turrets.Count; i++)
        {
            turrets[i].GetGameObject().transform.position = turretsPositions[i];
        }
    }
    

    public bool AddEntity(GameObject prefab, CharacterStats stats, Vector3 spawnPosition, string entityName)
    {
        // if the queue is larger than 10, we don't add the entity to spawn
        if (entitiesToSpawn.Count > 10) return false;
        entitiesToSpawn.Enqueue(new EntityToSpawn(prefab, this, stats, spawnPosition, entityName));
        return true;
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
            // if there are entities to spawn and there is less than 10 entities on the field
            if (entitiesToSpawn.Count > 0 && entities.Count < 10)
            {
                EntityToSpawn entityToSpawn = entitiesToSpawn.Dequeue();
                GameObject spawnedObject = Object.Instantiate(entityToSpawn.GetPrefab(),
                    entityToSpawn.GetSpawnPosition(), Quaternion.identity);
                spawnedObject.SetActive(true);
                spawnedObject.tag = "Untagged";

                if (GetSide().Equals(Side.Enemy)) spawnedObject.GetComponent<SpriteRenderer>().flipX = true;
                spawnedObject.name = entityToSpawn.GetEntityName();

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
            AddGold(Convert.ToInt32(Math.Round(10 * currentAge.GetGoldMultiplier() * currentAge.GetAdditionalIncomeMultiplier() * teamGoldMultiplier)));
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
        UpdateTurretPosition();
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

    // Todo refaire parce sinon lolo pas content
    public void ToggleLockEntityUi(int? entityIndexToToggle)
    {
        GameObject spawnEntitiesButtons = GameObject.Find("SpawnEntities");
        if (spawnEntitiesButtons == null)
        {
            Debug.LogError("SpawnEntities GameObject not found");
            return;
        }

        GameObject upgradeEntitesButtons =
            CustomGameObjects.FindMaybeDisabledGameObjectByName("Menus", "upgradeUnitsMenu");
        if (spawnEntitiesButtons == null)
        {
            Debug.LogError("upgradeUnitsMenu GameObject not found");
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

            existingLockObject =
                upgradeEntitesButtons.transform.GetChild(lockedEntityIndex.Value).Find("Lock").gameObject;
            if (existingLockObject != null)
            {
                Object.Destroy(existingLockObject);
            }

            if (entityIndexToToggle == lockedEntityIndex)
            {
                ToggleLockEntity(entityIndexToToggle);
                return;
            }

            ToggleLockEntity(entityIndexToToggle);
        }

        if (entityIndexToToggle == null) return;

        GameObject childToLock = spawnEntitiesButtons.transform.GetChild(entityIndexToToggle.Value).gameObject;
        GameObject lockPrefab = Resources.Load<GameObject>("Common/lock/Lock");
        GameObject lockObject = Object.Instantiate(lockPrefab, childToLock.transform.position, Quaternion.identity);
        lockObject.name = "Lock";
        lockObject.transform.SetParent(childToLock.transform);

        childToLock = upgradeEntitesButtons.transform.GetChild(entityIndexToToggle.Value).gameObject;
        lockPrefab = Resources.Load<GameObject>("Common/lock/Lock");
        lockObject = Object.Instantiate(lockPrefab, childToLock.transform.position, Quaternion.identity);
        lockObject.name = "Lock";
        lockObject.transform.SetParent(childToLock.transform);
        lockObject.GetComponent<RectTransform>().sizeDelta = childToLock.GetComponent<RectTransform>().sizeDelta;
        lockObject.transform.localScale = new Vector3(1, 1, 1);

        ToggleLockEntity(entityIndexToToggle);
    }

    public void CastSpells(Type spellType, SpellStats spellStats)
    {
        if (experience < spellStats.deploymentCost)
        {
            Debug.Log("Not enough experience to deploy this spell " + spellStats.GetName());
            return;
        }

        if (spellCooldowns > Time.time)
        {
            Debug.Log("Spell is on cooldown: " + spellStats.GetName());
            return;
        }

        RemoveExperience(spellStats.deploymentCost);
        spellCooldowns = Time.time + spellStats.cooldown / 1000;

        for (int i = 0; i < spellStats.spellCount; i++)
        {
            // Ajoutez cette boucle pour générer des entités sur plusieurs couches y
            for (int j = 0; j < spellStats.wavesCount; j++)
            {
                Vector2 spawnPosition = new Vector2(
                    Random.Range(-15, 40), // Réduisez la plage pour x
                    12 + j * 4 // Augmentez la valeur de y pour chaque couche
                );

                Spell spell = Activator.CreateInstance(spellType, this) as Spell;
                if (spell == null) continue;
                spell.GetGameObject().transform.position = spawnPosition;
                // unfreeze y position
                spell.GetGameObject().GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                spell.GetGameObject().SetActive(true);

                gameManager.AddSpell(spell);
            }
        }
    }

    public int? GetLockedEntityIndex()
    {
        return lockedEntityIndex;
    }

    public UpgradeUnits GetUpgradeUnits()
    {
        return upgradeUnits;
    }

    public UpgradeTurrets GetUpgradeTurrets()
    {
        return upgradeTurrets;
    }
    
    public void SetTeamGoldMultiplier(float multiplier)
    {
        teamGoldMultiplier = multiplier;
    }
}