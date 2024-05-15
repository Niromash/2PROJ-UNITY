using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Entity
{
    private GameObject gameObject;
    private GameObject healthBar;
    private Side side;
    private Rigidbody2D rb;
    private Entity collidedEntityForwards;
    private Entity collidedEntityBackwards;
    private GameManager gameManager;
    private CharacterStats stats;

    public Entity(GameObject go, Side side, CharacterStats stats, GameManager gameManager)
    {
        rb = go.GetComponent<Rigidbody2D>();
        this.stats = stats;
        gameObject = go;
        this.side = side;
        healthBar = gameObject.transform.GetChild(0).gameObject;
        this.gameManager = gameManager;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public Side GetSide()
    {
        return side;
    }
    
    public CharacterStats GetStats()
    {
        return stats;
    }

    public Rigidbody2D GetRigidbody()
    {
        return rb;
    }

    public void SetForewardCollide(Entity entity)
    {
        collidedEntityForwards = entity;
        GetRigidbody().isKinematic = entity != null;
    }

    public bool IsForewardColliding()
    {
        return collidedEntityForwards != null;
    }

    public Entity GetCollidedEntityForwards()
    {
        return collidedEntityForwards;
    }
    
    public void SetBackwardCollide(Entity entity)
    {
        collidedEntityBackwards = entity;
    }

    public bool IsBackwardColliding()
    {
        return collidedEntityBackwards != null;
    }
    
    public Entity GetCollidedEntityBackwards()
    {
        return collidedEntityBackwards;
    }

    
    public void UpdateHealthBar()
    {
        // Get the current health of the entity
        float currentHealth = stats.Health;

        // Calculate the health percentage
        float healthPercentage = currentHealth / 50f; // Assuming 100 is the maximum health

        // Get the health bar's current local scale
        Vector3 healthBarScale = healthBar.transform.localScale;

        // Set the x value of the health bar's local scale to the health percentage
        healthBarScale.x = healthPercentage;

        // Apply the new local scale to the health bar
        healthBar.transform.localScale = healthBarScale;
    }
    
    public void TakeDamage(Entity entity)
    {
        Debug.Log("healthAmount: " + entity.GetStats().Health);
        entity.GetStats().Health -= entity.GetStats().DamagePerSecond;

        // Update the health bar
        UpdateHealthBar();
    }
    
    public void Kill()
    {
        gameManager.RemoveEntity(this);
    }
}