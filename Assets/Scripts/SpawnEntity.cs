using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEntity : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject infantryPrefab;
    public GameObject antiArmorPrefab;
    public Vector2 spawnPosition;
    
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
        Spawn(infantryPrefab, playerTeam, new InfantryStats());
    }

    public void AntiArmorPlayerSpawn()
    {
        Team playerTeam = gameManager.GetTeams().Find(team => team.GetSide().Equals(Side.Player));
        Spawn(antiArmorPrefab, playerTeam, new AntiArmorStats());
    }

    private void Spawn(GameObject prefab, Team team, CharacterStats stats)
    {
        GameObject spawnedObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
        spawnedObject.SetActive(true);
        // remove the tag so that the spawned object is not considered a template
        spawnedObject.tag = "Untagged";

        Entity entity = new Entity(spawnedObject, team, stats, gameManager);
        gameManager.AddEntity(entity);
    }
}