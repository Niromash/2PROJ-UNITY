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
            return;
        }
        
        if (collidedSource.GetSide().Equals(collidedTarget.GetSide()))
        {
            Debug.Log("Same side, no damage taken");
            return;
        }
        
        // collidedSource.TakeDamage(collidedTarget);
        
        Debug.Log("Collision detected, damage taken");
    }
}
