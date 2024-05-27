using UnityEngine;

public class SpellCollisionController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Vérifiez si l'objet entrant en collision est le sol
        if (gameObject.transform.position.y <= 0)
        {
            // Détruisez l'objet
            Destroy(gameObject);
        }
    }
}