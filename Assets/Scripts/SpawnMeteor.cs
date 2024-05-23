using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnMeteor : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject meteorPrefab;
    public int numberOfMeteors = 20;

    private void spawnMeteor()
    {
        for (int i = 0; i < numberOfMeteors; i++)
        {
            Vector2 spawnPosition = new Vector2(
                Random.Range(0, 42), // x between 0 and 42
                12
            );

            GameObject spawnedObject = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);
            spawnedObject.SetActive(true);
        }
    }


    public void SpawnMeteors()
    {
        spawnMeteor();
    }
}