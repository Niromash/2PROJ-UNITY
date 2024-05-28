using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnSpell : MonoBehaviour
{
    public GameManager gameManager;

    public void SpawnMeteor()
    {
        Spawn(typeof(Meteor), new MeteorStats());
    }

    public void SpawnArrow()
    {
        Spawn(typeof(Arrow), new ArrowStats());
    }

    private void Spawn(Type spellType, SpellStats spellStats)
    {
        for (int i = 0; i < spellStats.spellCount; i++)
        {
            for (int j = 0; j < spellStats.wavesCount; j++) // Ajoutez cette boucle pour générer des entités sur plusieurs couches y
            {
                Vector2 spawnPosition = new Vector2(
                    Random.Range(-5, 28), // Réduisez la plage pour x
                    12 + j * 4 // Augmentez la valeur de y pour chaque couche
                );

                Spell spell = Activator.CreateInstance(spellType, new object[] { Side.Player, gameManager }) as Spell;
                if (spell == null) continue;
                spell.GetGameObject().transform.position = spawnPosition;
                // unfreeze y position
                spell.GetGameObject().GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                spell.GetGameObject().SetActive(true);

                gameManager.AddSpell(spell);
            }
        }
    }
}