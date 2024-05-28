using UnityEngine;

public class SpellCollisionController : MonoBehaviour
{
    public GameManager gameManager;

    // Using OnTriggerEnter2D instead of OnCollisionEnter2D to detect collision without impacting the physics
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Spell spell = gameManager.GetSpell(gameObject);
        if (spell == null) return;

        // Vérifiez si l'objet entrant en collision est le sol
        if (gameObject.transform.position.y <= 0 || collider.gameObject != null)
        {
            Spell collidedSpell = gameManager.GetSpell(collider.gameObject);
            if (collidedSpell == null)
            {
                // Détruisez l'objet
                gameManager.RemoveSpell(spell);
            }
        }

        // Vérifiez si l'objet entrant en collision est un ennemi
        GameObject collidedObject = collider.gameObject;
        if (collidedObject.CompareTag("Template")) return;
        Entity collidedEntity = gameManager.GetEntity(collidedObject);
        if (collidedEntity == null) return;

        // Apply the spell effect
        spell.ApplyEffect(collidedEntity);
    }
}