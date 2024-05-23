using UnityEngine;

public class Tower : Damageable
{
    private float health;
    private float maxHealth;
    private GameObject healthBar;
    private GameManager gameManager;
    private readonly GameObject towerGameObject;
    private readonly Team team;

    public Tower(float maxHealth, GameObject towerGameObject, Team team, GameManager gameManager)
    {
        healthBar = towerGameObject.transform.GetChild(0).gameObject;
        
        this.maxHealth = maxHealth;
        health = maxHealth;
        this.towerGameObject = towerGameObject;
        this.team = team;
        this.gameManager = gameManager;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Kill();
        }

        UpdateHealthBar();
    }

    public float GetHealth()
    {
        return health;
    }

    public void Kill()
    {
        // Destroy the tower, probably the end of the game
        gameManager.EndGame();
    }

    public Team GetTeam()
    {
        return team;
    }

    public GameObject GetGameObject()
    {
        return towerGameObject;
    }

    public void UpdateHealthBar()
    {
        // Get the current health of the entity
        float currentHealth = health;

        // Calculate the health percentage
        float healthPercentage = currentHealth / maxHealth;

        // Get the health bar's current local scale
        Vector3 healthBarScale = healthBar.transform.localScale;

        // Set the x value of the health bar's local scale to the health percentage
        healthBarScale.x = healthPercentage;

        // Apply the new local scale to the health bar
        healthBar.transform.localScale = healthBarScale;
    }
}