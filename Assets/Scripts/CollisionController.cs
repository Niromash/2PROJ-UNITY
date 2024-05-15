using System.Collections;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public GameManager gameManager;

    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter2D(Collision2D collision)
    {
        Entity collidedSource = gameManager.GetEntity(collision.gameObject);
        Entity collidedTarget = gameManager.GetEntity(gameObject);

        if (collidedSource == null || collidedTarget == null)
        {
            if (collidedSource == null)
            {
                HandleTowerCollision(collidedTarget, gameManager.GetTower(collision.gameObject));
            }
            else
            {
                HandleTowerCollision(collidedSource, gameManager.GetTower(gameObject));
            }
        }

        if (collidedSource == null || collidedTarget == null) return;

        // Determine which entity is in front and which is behind based on their x position
        Entity entityInFront, entityBehind;
        if (collidedSource.GetGameObject().transform.position.x >
            collidedTarget.GetGameObject().transform.position.x)
        {
            entityInFront = collidedSource;
            entityBehind = collidedTarget;
        }
        else
        {
            entityInFront = collidedTarget;
            entityBehind = collidedSource;
        }

        // Set the forward and backward collisions for the entities
        entityInFront.SetBackwardCollide(entityBehind);
        entityBehind.SetForewardCollide(entityInFront);

        if (collidedSource.GetTeam().GetSide().Equals(collidedTarget.GetTeam().GetSide()))
        {
            // Debug.Log("Same side, no damage taken");
            return;
        }

        if (collidedSource.GetStats().health > 0 && collidedTarget.GetStats().health > 0)
        {
            StartCoroutine(DamageOverTime(collidedSource, collidedTarget));
        }
    }

    IEnumerator DamageOverTime(Damageable damageable, Entity source)
    {
        while (source.GetStats().health > 0 && damageable.GetHealth() > 0)
        {
            damageable.TakeDamage(source.GetStats().damagePerSecond);
            yield return new WaitForSeconds(1);
        }

        if (damageable.GetHealth() <= 0)
        {
            damageable.Kill();
            if (damageable is Entity target)
            {
                target.SetForewardCollide(null);
                // Force all the backward entities to forward collide null
                StartCoroutine(RecursiveSetForewardCollide(target));
            }
        }

        if (source.GetStats().health <= 0)
        {
            source.Kill();
            if (damageable is Entity sourceEntity)
            {
                sourceEntity.SetBackwardCollide(null);
                // Force all the forward entities to backward collide null
                StartCoroutine(RecursiveSetForewardCollide(source));
            }
        }
    }

    IEnumerator RecursiveSetForewardCollide(Entity entity)
    {
        if (entity.GetCollidedEntityBackwards() != null)
        {
            entity.GetCollidedEntityBackwards().SetForewardCollide(null);
            yield return new WaitForSeconds(.5f); // Wait for 0.5 seconds before setting the next entity
            StartCoroutine(RecursiveSetForewardCollide(entity.GetCollidedEntityBackwards()));
        }
    }

    private void HandleTowerCollision(Entity entity, Tower tower)
    {
        if (tower == null) return;

        if (tower.GetTeam().GetSide().Equals(entity.GetTeam().GetSide())) return;

        Debug.Log(tower.GetTeam().GetSide() + " tower has been hit by " + entity.GetTeam().GetSide());

        StartCoroutine(DamageOverTime(tower, entity));
    }
}