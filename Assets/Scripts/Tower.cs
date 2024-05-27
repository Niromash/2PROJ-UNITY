using UnityEngine;
using UnityEngine.Tilemaps;

public class Tower : Damageable
{
    private float health;
    private float maxHealth;
    private GameObject healthBar;
    private GameManager gameManager;
    private readonly GameObject towerGameObject;
    private readonly Tilemap tileMap;
    private readonly Vector3Int minCellPosition;
    private readonly Team team;

    public Tower(float maxHealth, GameObject towerGameObject, Team team, GameManager gameManager)
    {
        healthBar = GameObject.Find(team.GetSide().Equals(Side.Player) ? "TowerLeftHealthBar" : "TowerRightHealthBar");
        tileMap = towerGameObject.GetComponent<Tilemap>();

        // Get the position of the most left/right tile of the tower depending on the team
        minCellPosition = GetExtremeTilePosition(tileMap, team.GetSide().Equals(Side.Enemy));

        this.maxHealth = maxHealth;
        health = maxHealth;
        this.towerGameObject = towerGameObject;
        this.team = team;
        this.gameManager = gameManager;
    }

    // Function to find the extreme tile position (leftmost or rightmost) of the tower
    private Vector3Int GetExtremeTilePosition(Tilemap tileMap, bool findLeftmost)
    {
        int extremeX = findLeftmost ? int.MaxValue : int.MinValue;
        Vector3Int extremeCell = Vector3Int.zero;

        foreach (Vector3Int pos in tileMap.cellBounds.allPositionsWithin)
        {
            if (tileMap.HasTile(pos))
            {
                if ((findLeftmost && pos.x < extremeX) || (!findLeftmost && pos.x > extremeX))
                {
                    extremeX = pos.x;
                    extremeCell = pos;
                }
            }
        }

        return extremeCell;
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

    public Vector3 GetPosition()
    {
        return tileMap.GetCellCenterWorld(minCellPosition);
    }

    public Vector3 GetSize()
    {
        // tileMap.CompressBounds();
        return tileMap.size;
    }
}