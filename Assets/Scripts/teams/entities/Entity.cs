using UnityEngine;
using UnityEngine.UI;

public class Entity : Damageable, Damager
{
    private readonly GameObject gameObject;
    private readonly Image healthBarImage;
    private readonly Team team;
    private readonly Rigidbody2D rb;
    private readonly Collider2D collider2d;
    private Entity collidedEntityForwards;
    private Entity collidedEntityBackwards;
    private Tower collidedTowerForwards;
    private readonly CharacterStats stats;
    private bool isKilled;
    private readonly GameManager gameManager;

    public Entity(GameObject go, Team team, CharacterStats stats, GameManager gameManager)
    {
        rb = go.GetComponent<Rigidbody2D>();
        collider2d = go.GetComponent<Collider2D>();
        this.stats = stats;
        gameObject = go;
        this.team = team;
        this.gameManager = gameManager;
        healthBarImage = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>();

        if (stats.GetEntityType().Equals(EntityTypes.Tank) && this.team.GetCurrentAge().GetAgeLevel() == 2)
        {
            this.stats.range = 5f;
        }

        stats.ApplyMultiplier(team.GetCurrentAge()); // Update stats with age multiplier

        UnitUpgrade unitUpgrade = team.GetUpgradeUnits().GetUnitUpgrade(stats.GetEntityType());
        if (unitUpgrade != null) stats.ApplyMultiplier(unitUpgrade);

        UpdateHealthBar();
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

    public Collider2D GetCollider()
    {
        return collider2d;
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
        // Calculez le pourcentage de santé restant
        float healthPercentage = stats.health / stats.maxHealth;

        healthBarImage.color = Color.Lerp(Color.red, Color.green, healthPercentage);
        healthBarImage.fillAmount = healthPercentage;
    }

    public void TakeDamage(Damager damager)
    {
        float factor = 1;

        if (damager is Entity entity)
        {
            EntityStrengthWeakness entityStrengthWeakness = gameManager.GetEntityStrengthWeakness();
            factor = entityStrengthWeakness.GetStrengthWeakness(stats.GetEntityType(),
                entity.GetStats().GetEntityType());
        }

        stats.health -= damager.GetDamagerStats().GetDamage() * factor;
        if (stats.health <= 0)
        {
            Kill(damager);
        }

        DamageIndicator damageIndicator = gameObject.AddComponent<DamageIndicator>();
        damageIndicator.damageTextPrefab = GameObject.Find("DamageValue");
        damageIndicator.canvasTransform = GameObject.Find("DamageCanvas").transform;
        damageIndicator.efficiency = factor;
        damageIndicator.ShowDamage(damager.GetDamagerStats().GetDamage(), gameObject.transform.position);

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
        return collider2d.bounds.size;
    }

    public DamagerStats GetDamagerStats()
    {
        return stats;
    }

    public string GetName()
    {
        return stats.GetName();
    }
    
    public EntityTypes GetEntityType()
    {
        return stats.GetEntityType();
    }
}