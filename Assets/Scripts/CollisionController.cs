using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
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

        // StartCoroutine(CheckForEnemiesInRange());
    }

    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter2D(Collision2D collision)
    {
        Entity collidedSource = gameManager.GetEntity(collision.gameObject);
        Entity collidedTarget = sourceEntity;

        if (collidedSource == null || collidedTarget == null)
        {
            if (collidedSource == null)
            {
                if (HandleTowerCollision(collidedTarget, gameManager.GetTower(collision.gameObject))) return;
                if (HandleMeteorCollision(gameManager.GetMeteor(collision.gameObject))) return;
            }
            else
            {
                if (HandleTowerCollision(collidedSource, gameManager.GetTower(gameObject))) return;
                if (HandleMeteorCollision(gameManager.GetMeteor(gameObject))) return;
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
        // if the side is enemy, inverse the order
        if (entityInFront.GetTeam().GetSide().Equals(Side.Enemy))
        {
            (entityInFront, entityBehind) = (entityBehind, entityInFront);
        }

        if (entityBehind.GetTeam().GetSide().Equals(entityInFront.GetTeam().GetSide()))
        {
            entityInFront.SetBackwardCollide(entityBehind);
            entityBehind.SetForwardCollide(entityInFront);
            return;
        }

        entityInFront.SetForwardCollide(entityBehind);
        entityBehind.SetForwardCollide(entityInFront);

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
            source.SetForwardCollide(null);
        }

        if (source.GetStats().health <= 0)
        {
            source.Kill();
            if (damageable is Entity entity)
            {
                entity.SetForwardCollide(null);
            }
        }
    }

    private bool HandleTowerCollision(Entity entity, Tower tower)
    {
        if (tower == null) return false;
        if (tower.GetTeam().GetSide().Equals(entity.GetTeam().GetSide())) return false;

        entity.SetCollidedTowerForwards(tower);
        Debug.Log(tower.GetTeam().GetSide() + " tower has been hit by " + entity.GetTeam().GetSide());
        StartCoroutine(DamageOverTime(tower, entity));

        return true;
    }

    private bool HandleMeteorCollision(Meteor meteor)
    {
        if (meteor == null) return false;

        Debug.Log("Meteor collided: " + meteor.GetGameObject().name);
        meteor.GetGameObject().SetActive(false);
        Destroy(meteor.GetGameObject());

        return true;
    }
}