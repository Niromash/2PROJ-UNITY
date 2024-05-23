using UnityEngine;

public class Entity : Damageable
{
    private GameObject gameObject;
    private GameObject healthBar;
    private Team team;
    private Rigidbody2D rb;
    private Entity collidedEntityForwards;
    private Entity collidedEntityBackwards;
    private GameManager gameManager;
    private CharacterStats stats;
    private bool isKilled;
    

    public Entity(GameObject go, Team team, CharacterStats stats, GameManager gameManager)
    {
        rb = go.GetComponent<Rigidbody2D>();
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

    public void SetForewardCollide(Entity entity)
    {
        collidedEntityForwards = entity;
        if (!isKilled)
            GetRigidbody().isKinematic =
                entity != null; // this method can be called after delay, so we need to check if the rigidbody is not null
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
        gameManager.RemoveEntity(this);
        if (team.GetSide() == Side.Enemy)
            gameManager.GainGoldByKill();
            gameManager.GainExpByKill();
    }
    
    public bool IsKilled()
    {
        return isKilled;
    }
}