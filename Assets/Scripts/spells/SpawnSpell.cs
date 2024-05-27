using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnSpell : MonoBehaviour
{
    public GameManager gameManager;

    public void SpawnMeteor()
    {
        for (int i = 0; i < 20; i++)
        {
            Vector2 spawnPosition = new Vector2(
                Random.Range(0, 42), // x between 0 and 42
                12
            );

            Spell spell = new Meteor(Side.Player, gameManager);
            spell.GetGameObject().transform.position = spawnPosition;
            // unfreeze y position
            spell.GetGameObject().GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            spell.GetGameObject().SetActive(true);

            gameManager.AddSpell(spell);
        }
    }
}