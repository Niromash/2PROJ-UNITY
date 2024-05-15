using System;
using System.Collections;
using System.Collections.Generic;
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
                Turret turret = gameManager.GetTurret(collision.gameObject);
                if (turret == null)
                {
                    return;
                }

                // la target à tapé un turret
                Debug.Log(turret.GetSide());
            }
        }

        if (collidedSource == null || collidedTarget == null)
        {
            return;
        }

        // Determine which entity is in front and which is behind based on their x position
        Entity entityInFront, entityBehind;
        if (collidedSource.GetGameObject().transform.position.x > collidedTarget.GetGameObject().transform.position.x)
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

        if (collidedSource.GetSide().Equals(collidedTarget.GetSide()))
        {
            // Debug.Log("Same side, no damage taken");
            return;
        }
        
        if (collidedSource.GetStats().Health > 0 && collidedTarget.GetStats().Health > 0)
        {
            StartCoroutine(DamageOverTime(collidedSource, collidedTarget));
        }
    }

    IEnumerator DamageOverTime(Entity source, Entity target)
    {
        while (source.GetStats().Health > 0 && target.GetStats().Health > 0)
        {
            target.TakeDamage(source);
            yield return new WaitForSeconds(1);
        }
        
        if (source.GetStats().Health <= 0)
        { 
            source.Kill();   
            target.SetForewardCollide(null);
            // Force all the backward entities to forward collide null
            StartCoroutine(RecursiveSetForewardCollide(source));
        }
        if (target.GetStats().Health <= 0)
        {
            target.Kill();
            source.SetForewardCollide(null);
            // Force all the backward entities to forward collide null
            StartCoroutine(RecursiveSetForewardCollide(target));
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
}