using System;
using UnityEngine;

public class BulletCollisionController : MonoBehaviour
{
    public GameManager gameManager;
    private BulletMetadata bulletMetadata;
    public AudioSource popAudio;

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
                if (collidedEntity.GetTeam().GetSide()
                    .Equals(bulletMetadata.GetSourceTurret().GetTeam().GetSide())) return;
                collidedEntity.TakeDamage(bulletMetadata.GetSourceTurret());
                popAudio.Play();
                Destroy(gameObject);
            }
        }
    }
}