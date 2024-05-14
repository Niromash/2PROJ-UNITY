using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEntity : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject infantryPrefab;
    public GameObject antiArmorPrefab;
    public Vector2 spawnPosition;


    public void InfantryPlayerSpawn()
    {
        Spawn(infantryPrefab, Side.Player);
    }

    public void InfantryEnemySpawn()
    {
        Spawn(infantryPrefab, Side.Enemy);
    }

    public void AntiArmorPlayerSpawn()
    {
        Spawn(antiArmorPrefab, Side.Player);
    }
    
    public void AntiArmorEnemySpawn()
    {
        Spawn(antiArmorPrefab, Side.Enemy);
    }

    private void Spawn(GameObject prefab, Side side)
    {
        GameObject spawnedObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
        spawnedObject.SetActive(true);
        
        gameManager.AddEntity(new Entity(spawnedObject, side));
    }
}