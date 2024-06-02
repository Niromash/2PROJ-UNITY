using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public GameManager gameManager;
    private Entity sourceEntity;

    void Start()
    {
        if (sourceEntity != null) return;
        // if the gameobject has tag "template" then it is a template and should not be used
        if (gameObject.CompareTag("Template")) return;
        sourceEntity = gameManager.GetEntity(gameObject);

        if (sourceEntity == null)
        {
            Debug.LogError("Entity not found for " + gameObject.name);
            return;
        }

        StartCoroutine(CheckForEnemiesInRange());
    }

    // GetEnemiesInRange returns a list of enemies within a certain range (including entites and towers)
    public List<Damageable> GetEnemiesInRange(float range)
    {
        List<Damageable> enemiesInRange = new List<Damageable>();

        // Cast a circle from the entity's position with a radius equal to the range
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, Vector2.zero);
        foreach (var hit in hits)
        {
            Damageable iteratedEntity = gameManager.GetEntity(hit.collider.gameObject);
            if (iteratedEntity == null)
            {
                iteratedEntity = gameManager.GetTower(hit.collider.gameObject);
            }

            if (iteratedEntity != null && !iteratedEntity.GetTeam().GetSide().Equals(sourceEntity.GetTeam().GetSide()))
            {
                // Calculate the distance considering the size of the entity
                float distanceToEntity = Vector2.Distance(transform.position, iteratedEntity.GetPosition());
                float entityRadius = iteratedEntity.GetSize().x / 2;

                // Check if the distance is within the adjusted range
                if (distanceToEntity <= range + entityRadius)
                {
                    enemiesInRange.Add(iteratedEntity);
                }
            }
        }

        // Sort the list by distance
        enemiesInRange.Sort((a, b) =>
        {
            float distanceA = Vector2.Distance(transform.position, a.GetPosition());
            float distanceB = Vector2.Distance(transform.position, b.GetPosition());
            return distanceA.CompareTo(distanceB);
        });

        return enemiesInRange;
    }

    private IEnumerator CheckForEnemiesInRange()
    {
        // Check for enemies in range every second while the game is playing
        while (GameManager.GetGameState() == GameState.Playing)
        {
            List<Damageable> enemiesInRange = GetEnemiesInRange(sourceEntity.GetStats().range);
            if (enemiesInRange.Count > 0)
            {
                // Attack the first enemy in the list, which is the closest one
                enemiesInRange[0].TakeDamage(sourceEntity);
            }

            yield return new WaitForSeconds(1);
        }
    }
}