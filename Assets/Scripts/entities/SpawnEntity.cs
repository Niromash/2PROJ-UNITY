using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEntity : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject tankPrefab;
    public GameObject infantryPrefab;
    public GameObject antiArmorPrefab;
    public GameObject supportPrefab;
    public Vector2 spawnPosition;
    private int infantryCount = 0;
    private int antiArmorCount = 0;

    
    public void TankPlayerSpawn()
    {
        Team playerTeam = gameManager.GetTeams().Find(team => team.GetSide().Equals(Side.Player));
        Spawn(tankPrefab, playerTeam, new TankStats());
    }
    
    public void InfantryPlayerSpawn()
    {
        Team playerTeam = gameManager.GetTeams().Find(team => team.GetSide().Equals(Side.Player));
        Spawn(infantryPrefab, playerTeam, new InfantryStats());
    }

    public void AntiArmorPlayerSpawn()
    {
        Team playerTeam = gameManager.GetTeams().Find(team => team.GetSide().Equals(Side.Player));
        Spawn(antiArmorPrefab, playerTeam, new AntiArmorStats());
    }
    
    public void SupportPlayerSpawn()
    {
        Team playerTeam = gameManager.GetTeams().Find(team => team.GetSide().Equals(Side.Player));
        Spawn(supportPrefab, playerTeam, new SupportStats());
    }

    private void Spawn(GameObject prefab, Team team, CharacterStats stats)
    {
        stats.ApplyMultiplier(team.GetCurrentAge()); // Update stats with age multiplier
        if (stats.deploymentCost > team.GetGold())
        {
            Debug.Log("Not enough gold to spawn entity " + prefab.name);
            return;
        }

        string entityName;
        // Increment the counter for the entity type and add it to the name
        if (prefab.name == "Infantry")
        {
            infantryCount++;
            entityName = prefab.name + infantryCount;
        }
        else if (prefab.name == "AntiArmor")
        {
            antiArmorCount++;
            entityName = prefab.name + antiArmorCount;
        }
        else if (prefab.name == "Tank")
        {
            entityName = prefab.name;
        }
        else if (prefab.name == "Support")
        {
            entityName = prefab.name;
        }
        else
        {
            entityName = prefab.name;
        }

        team.AddEntity(prefab, stats, spawnPosition, entityName);
        team.RemoveGold(stats.deploymentCost);
    }
}