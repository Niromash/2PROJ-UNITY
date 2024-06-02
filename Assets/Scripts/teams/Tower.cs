using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Tower : Damageable
{
    private float health;
    private float maxHealth;
    private Image healthBarImage;
    private GameManager gameManager;
    private readonly GameObject towerGameObject;
    private readonly Tilemap tileMap;
    private readonly Vector3Int minCellPosition;
    private readonly Team team;
    private List<Turret> turrets;

    public Tower(float maxHealth, GameObject towerGameObject, Team team, GameManager gameManager)
    {
        GameObject healthBar =
            GameObject.Find(team.GetSide().Equals(Side.Player) ? "TowerLeftHealthBar" : "TowerRightHealthBar");
        healthBarImage = healthBar.GetComponent<Image>();

        tileMap = towerGameObject.GetComponent<Tilemap>();
        turrets = new List<Turret>();

        for (int i = 0; i < 4; i++)
        {
            GameObject turretGameObject = towerGameObject.transform.GetChild(i).gameObject;
            Turret newTurret = new Turret(turretGameObject, new TurretStats(), team, i);
            turrets.Add(newTurret);
        }

        // Get the position of the most left/right tile of the tower depending on the team
        minCellPosition = GetExtremeTilePosition(tileMap, team.GetSide().Equals(Side.Enemy));

        this.maxHealth = maxHealth;
        health = maxHealth;
        this.towerGameObject = towerGameObject;
        this.team = team;
        this.gameManager = gameManager;

        UpdateHealthBar();
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

    public void TakeDamage(Damager damager)
    {
        health -= damager.GetDamagerStats().GetDamage();
        if (health <= 0)
        {
            Kill(damager);
        }

        DamageIndicator damageIndicator = towerGameObject.AddComponent<DamageIndicator>();
        damageIndicator.damageTextPrefab = GameObject.Find("DamageValue");
        damageIndicator.canvasTransform = GameObject.Find("DamageCanvas").transform;
        damageIndicator.ShowDamage(damager.GetDamagerStats().GetDamage(), GetPosition());

        UpdateHealthBar();
    }

    public float GetHealth()
    {
        return health;
    }

    public void Kill(Damager damager)
    {
        // Destroy the tower, probably the end of the game
        gameManager.EndGame(damager);
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
        // Calculez le pourcentage de santé restant
        float healthPercentage = health / maxHealth;

        healthBarImage.color = Color.Lerp(Color.red, Color.green, healthPercentage);
        healthBarImage.fillAmount = healthPercentage;
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

    public void AddTurret(Turret turret)
    {
        turrets.Add(turret);
    }

    public List<Turret> GetTurrets()
    {
        return turrets;
    }
}