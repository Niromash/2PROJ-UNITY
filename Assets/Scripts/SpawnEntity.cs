using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEntity : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject infantryPrefab;
    public GameObject antiArmorPrefab;
    public Vector2 spawnPosition;
    
    private int infantryCount = 0;
    private int antiArmorCount = 0;
    
    // public void InfantryEnemySpawn()
    // {
    //     Spawn(infantryPrefab, Side.Enemy);
    // }
    //
    // public void AntiArmorEnemySpawn()
    // {
    //     Spawn(antiArmorPrefab, Side.Enemy);
    // }
    
    public void InfantryPlayerSpawn()
    {
        Team playerTeam = gameManager.GetTeams().Find(team => team.GetSide().Equals(Side.Player));
        Spawn(infantryPrefab, playerTeam, new InfantryStats(), "Infantry");
    }

    public void AntiArmorPlayerSpawn()
    {
        Team playerTeam = gameManager.GetTeams().Find(team => team.GetSide().Equals(Side.Player));
        Spawn(antiArmorPrefab, playerTeam, new AntiArmorStats(), "AntiArmor");
    }

    private void Spawn(GameObject prefab, Team team, CharacterStats stats, string entityType)
    {
        GameObject spawnedObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
        spawnedObject.SetActive(true);

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

        Entity entity = new Entity(spawnedObject, team, stats, gameManager);
        gameManager.AddEntity(entity);
    }
}