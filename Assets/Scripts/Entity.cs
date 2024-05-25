using UnityEngine;

public class Entity : Damageable
{
    private readonly GameObject gameObject;
    private readonly GameObject healthBar;
    private readonly Team team;
    private readonly Rigidbody2D rb;
    private readonly SpriteRenderer spriteRenderer;
    private Entity collidedEntityForwards;
    private Entity collidedEntityBackwards;
    private Tower collidedTowerForwards;
    private readonly GameManager gameManager;
    private readonly CharacterStats stats;
    private bool isKilled;

    public Entity(GameObject go, Team team, CharacterStats stats, GameManager gameManager)
    {
        rb = go.GetComponent<Rigidbody2D>();
        spriteRenderer = go.GetComponent<SpriteRenderer>();
        this.stats = stats;
        gameObject = go;
        this.team = team;
        healthBar = gameObject.transform.GetChild(0).gameObject;
        this.gameManager = gameManager;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public Team GetTeam()
    {
        return team;
    }

    public CharacterStats GetStats()
    {
        return stats;
    }

    public Rigidbody2D GetRigidbody()
    {
        return rb;
    }
    
    public SpriteRenderer GetSpriteRenderer()
    {
        return spriteRenderer;
    }
    
    public void SetForwardCollide(Entity entity)
    {
        collidedEntityForwards = entity;
        if (!isKilled)
        {
            // this method can be called after delay, so we need to check if the rigidbody is not null (if the entity is killed, the rigidbody is set to null)
            GetRigidbody().isKinematic = entity != null;
        }
    }

    public bool IsForwardColliding()
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

    public void SetCollidedTowerForwards(Tower tower)
    {
        collidedTowerForwards = tower;
    }

    public Tower GetCollidedTowerForwards()
    {
        return collidedTowerForwards;
    }

    public void UpdateHealthBar()
    {
        // Get the current health of the entity
        float currentHealth = stats.health;

        // Calculate the health percentage
        float healthPercentage = currentHealth / 50f; // Assuming 100 is the maximum health

        // Get the health bar's current local scale
        Vector3 healthBarScale = healthBar.transform.localScale;

        // Set the x value of the health bar's local scale to the health percentage
        healthBarScale.x = healthPercentage;

        // Apply the new local scale to the health bar
        healthBar.transform.localScale = healthBarScale;
    }

    public void TakeDamageFromEntity(Entity entity)
    {
        TakeDamage(entity.GetStats().damagePerSecond);
    }

    public void TakeDamage(float damage)
    {
        stats.health -= damage;
        if (stats.health <= 0)
        {
            Kill();
        }

        UpdateHealthBar();
    }

    public float GetHealth()
    {
        return stats.health;
    }

    public void Kill()
    {
        isKilled = true;
        
        // set to the backward entity, the forward entity null
        if (collidedEntityBackwards != null)
        {
            collidedEntityBackwards.SetForwardCollide(null);
        }
        
        // set the forward entity, the forward entity null
        if (collidedEntityForwards != null)
        {
            collidedEntityForwards.SetForwardCollide(null);
        }
        
        gameManager.RemoveEntity(this);
    }

    public bool IsKilled()
    {
        return isKilled;
    }
}