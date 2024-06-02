using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretAttackController : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject bulletPrefab;
    private Turret sourceTurret;

    void Start()
    {
        if (sourceTurret != null) return;
        if (!gameObject.activeSelf) return; // Check if the turret is enabled

        sourceTurret = gameManager.GetTurret(gameObject);
        if (sourceTurret == null)
        {
            Debug.LogError("Turret not found for " + gameObject.name);
            return;
        }

        StartCoroutine(CheckForEnemiesInRange());
    }

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
                continue;
            }

            if (iteratedEntity != null && !iteratedEntity.GetTeam().GetSide().Equals(sourceTurret.GetTeam().GetSide()))
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

        // Sort the list by the distance to the tower
        enemiesInRange.Sort((a, b) =>
        {
            float distanceA = Vector2.Distance(sourceTurret.GetTeam().GetTower().GetPosition(), a.GetPosition());
            float distanceB = Vector2.Distance(sourceTurret.GetTeam().GetTower().GetPosition(), b.GetPosition());
            return distanceA.CompareTo(distanceB);
        });
        return enemiesInRange;
    }

    private IEnumerator CheckForEnemiesInRange()
    {
        while (GameManager.GetGameState() == GameState.Playing)
        {
            List<Damageable> enemiesInRange = GetEnemiesInRange(sourceTurret.GetStats().range);
            if (enemiesInRange.Count > 0)
            {
                var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.AddComponent<BulletMetadata>().SetSourceTurret(sourceTurret);

                Rigidbody2D rigidbody2D = bullet.GetComponent<Rigidbody2D>();
                rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

                // Get the first enemy in the list
                Damageable firstEnemy = enemiesInRange[0];

                // Calculate the direction towards the top of the enemy
                Vector2 direction = firstEnemy.GetPosition() - transform.position;
                direction.Normalize();
                
                // Add force to the bullet
                rigidbody2D.AddForce(direction * sourceTurret.GetStats().bulletSpeed, ForceMode2D.Impulse);

                // Add gravity to the bullet
                rigidbody2D.gravityScale = 1;
            }

            yield return new WaitForSeconds(1);
        }
    }
}