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
        
        collidedSource.SetCollide(collidedTarget);
        collidedTarget.SetCollide(collidedSource);
        
        if (collidedSource.GetSide().Equals(collidedTarget.GetSide()))
        {
            Debug.Log("Same side, no damage taken");
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
        }
        if (target.GetStats().Health <= 0)
        {
            target.Kill();
        }
    }
}