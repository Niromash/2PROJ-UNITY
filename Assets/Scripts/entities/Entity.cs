using UnityEngine;
using UnityEngine.UI;

public class Entity : Damageable, Damager
{
    private readonly GameObject gameObject;
    private readonly Image healthBarImage;
    private readonly Team team;
    private readonly Rigidbody2D rb;
    private readonly SpriteRenderer spriteRenderer;
    private Entity collidedEntityForwards;
    private Entity collidedEntityBackwards;
    private Tower collidedTowerForwards;
    private readonly CharacterStats stats;
    private readonly Age age;
    private bool isKilled;

    public Entity(GameObject go, Team team, CharacterStats stats)
    {
        rb = go.GetComponent<Rigidbody2D>();
        spriteRenderer = go.GetComponent<SpriteRenderer>();
        this.stats = stats;
        gameObject = go;
        this.team = team;
        age = team.GetCurrentAge();
        healthBarImage = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>();

        // Update stats with age multiplier
        stats.ApplyMultiplier(age.entitiesStatsMultiplier);
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
        // Calculez le pourcentage de santé restant
        float healthPercentage = stats.health / stats.maxHealth;

        healthBarImage.color = Color.Lerp(Color.red, Color.green, healthPercentage);
        healthBarImage.fillAmount = healthPercentage;
    }

    public void TakeDamage(Damager damager)
    {
        stats.health -= damager.GetDamagerStats().GetDamage();
        if (stats.health <= 0)
        {
            Kill(damager);
        }

        DamageIndicator damageIndicator = gameObject.AddComponent<DamageIndicator>();
        damageIndicator.damageTextPrefab = GameObject.Find("DamagePreview");
        damageIndicator.canvasTransform = GameObject.Find("DamageCanvas").transform;
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