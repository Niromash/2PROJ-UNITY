using UnityEngine;

public class Entity : Damageable, Damager, Nameable
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

    public float GetWidth()
    {
        // Assuming the entity's size is determined by its GameObject's sprite renderer
        return gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
    }
    
    public bool IsInRange(Entity other)
    {
        float distance = Vector3.Distance(this.GetGameObject().transform.position, other.GetGameObject().transform.position);
        // Subtract half of each entity's width from the distance
        distance -= this.GetWidth() / 2;
        distance -= other.GetWidth() / 2;
        return distance <= this.GetStats().range;
    }

    public void TakeDamage(Damager damager)
    {
        stats.health -= damager.GetDamagerStats().GetDamage();
        if (stats.health <= 0)
        {
            Kill(damager);
        }

        UpdateHealthBar();
    }

    public float GetHealth()
    {
        return stats.health;
    }

    public void Kill(Damager damager)
    {
        isKilled = true;
        
        // set to the backward entity, the forward entity null
        if (collidedEntityBackwards != null)
        {
            collidedEntityBackwards.SetForwardCollide(null);
        }
        
        // if the entity forward is enemy, set the forward entity null for the forward entity of the killed entity
        if (collidedEntityForwards != null && !collidedEntityForwards.GetTeam().GetSide().Equals(team.GetSide()))
        {
            collidedEntityForwards.SetForwardCollide(null);
        }
        
        team.RemoveEntity(this);
        damager.GetTeam().AddExperience(stats.deathExperience);
        damager.GetTeam().AddGold(stats.deathGold);
    }

    public bool IsKilled()
    {
        return isKilled;
    }
    
    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }
    
    public Vector3 GetSize()
    {
        return spriteRenderer.bounds.size;
    }

    public DamagerStats GetDamagerStats()
    {
        return stats;
    }

    public string GetName()
    {
        return stats.name;
    }
}