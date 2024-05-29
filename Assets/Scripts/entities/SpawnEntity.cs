using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEntity : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject tankPrefab;
    public GameObject infantryPrefab;
    public GameObject antiArmorPrefab;
    public Vector2 spawnPosition;
    
    public void TankPlayerSpawn()
    {
        Team playerTeam = gameManager.GetTeams().Find(team => team.GetSide().Equals(Side.Player));
        Spawn(tankPrefab, playerTeam, new TankStats());
    }
    
    private int infantryCount = 0;
    private int antiArmorCount = 0;
    
    
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

    private void Spawn(GameObject prefab, Team team, CharacterStats stats)
    {
        if (stats.deploymentCost > team.GetGold())
        {
            Debug.Log("Not enough gold to spawn entity " + prefab.name);
            return;
        }

        // Increment the counter for the entity type and add it to the name
        if (entityType == "Infantry")
        {
            infantryCount++;
            spawnedObject.name = entityType + infantryCount;
        }
        else if (entityType == "AntiArmor")
        {
            antiArmorCount++;
            spawnedObject.name = entityType + antiArmorCount;
        }

        team.AddEntity(prefab, stats, spawnPosition);
        team.RemoveGold(stats.deploymentCost);
    }
}