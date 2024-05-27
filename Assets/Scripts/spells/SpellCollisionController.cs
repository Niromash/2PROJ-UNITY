using UnityEngine;

public class SpellCollisionController : MonoBehaviour
{
    public GameManager gameManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Spell spell = gameManager.GetSpell(gameObject);
        if (spell == null) return;

        // Vérifiez si l'objet entrant en collision est le sol
        if (gameObject.transform.position.y <= 0 || collision.gameObject != null)
        {
            Spell collidedSpell = gameManager.GetSpell(collision.gameObject);
            if (collidedSpell == null)
            {
                // Détruisez l'objet
                gameManager.RemoveSpell(spell);
            }
        }

        // Vérifiez si l'objet entrant en collision est un ennemi
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.CompareTag("Template")) return;
        Entity collidedEntity = gameManager.GetEntity(collidedObject);
        if (collidedEntity == null) return;

        // Apply the spell effect
        spell.ApplyEffect(collidedEntity);
    }
}