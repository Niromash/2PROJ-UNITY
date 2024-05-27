using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnSpell : MonoBehaviour
{
    public readonly GameManager gameManager;
    public readonly GameObject spellPrefab;

    public void SpawnMeteor()
    {
        for (int i = 0; i < 20; i++)
        {
            Vector2 spawnPosition = new Vector2(
                Random.Range(0, 42), // x between 0 and 42
                12
            );

            Meteor meteor = new Meteor(Side.Player, gameManager);
            meteor.GetGameObject().transform.position = spawnPosition;
            // unfreeze y position
            meteor.GetGameObject().GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            meteor.GetGameObject().SetActive(true);
        }
    }
    
}