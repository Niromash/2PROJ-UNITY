using System;
using UnityEngine;

public class BulletCollisionController : MonoBehaviour
{
    public GameManager gameManager;
    private BulletMetadata bulletMetadata;

    public void Start()
    {
        bulletMetadata = gameObject.GetComponent<BulletMetadata>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (gameObject.transform.position.y <= 0 || collider.gameObject != null)
        {
            Entity collidedEntity = gameManager.GetEntity(collider.gameObject);
            if (collidedEntity != null)
            {
                collidedEntity.TakeDamage(bulletMetadata.GetSourceTurret());
                Destroy(gameObject);
            }
        }
    }
}