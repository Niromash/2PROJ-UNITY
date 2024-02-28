using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEntity : MonoBehaviour
{

    public GameManager gameManager;
    public GameObject spawnObjectPrefab; 
    public Vector2 spawnPosition;

    public void Click()
    {
        GameObject spawnedObject = Instantiate(spawnObjectPrefab, spawnPosition, Quaternion.identity);
        
        gameManager.AddEntity(spawnedObject);
    }
}
