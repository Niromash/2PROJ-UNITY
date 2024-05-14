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
        Spawn(infantryPrefab, Side.Player, new InfantryStats());
    }

    public void AntiArmorPlayerSpawn()
    {
        Spawn(antiArmorPrefab, Side.Player, new AntiArmorStats());
    }

    private void Spawn(GameObject prefab, Side side, CharacterStats stats)
    {
        GameObject spawnedObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
        spawnedObject.SetActive(true);

        Entity entity = new Entity(spawnedObject, side, stats);
        
        Debug.Log("stats vie: " + entity.GetStats().Health);

        gameManager.AddEntity(entity);
    }
}