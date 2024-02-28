using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEntity : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject infantryPrefab;
    public GameObject antiArmorPrefab;
    public Vector2 spawnPosition;


    public void InfantrySpawn()
    {
        Debug.Log("Infantry Spawn");
        Spawn(infantryPrefab);
    }

    public void AntiArmorSpawn()
    {
        Debug.Log("AntiArmor Spawn");
        Spawn(antiArmorPrefab);
    }

    private void Spawn(GameObject prefab)
    {
        GameObject spawnedObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
        spawnedObject.SetActive(true);
        gameManager.AddEntity(spawnedObject);
    }
}