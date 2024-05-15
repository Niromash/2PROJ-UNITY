using UnityEngine;

public class Entity
{
    private GameObject gameObject;
    private GameObject healthBar;
    private Side side;
    private float healthAmount;
    private Rigidbody2D rb;
    private Entity collidedEntityForwards;
    private Entity collidedEntityBackwards;

    public Entity(GameObject go, Side side)
    {
        rb = go.GetComponent<Rigidbody2D>();

        gameObject = go;

        healthAmount = 100;
        this.side = side;

        healthBar = gameObject.transform.GetChild(0).gameObject;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public Side GetSide()
    {
        return side;
    }

    public float GetHealth()
    {
        return healthAmount;
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

    public void TakeDamage(Entity entity)
    {
        healthAmount -= 10;
        healthBar.transform.localScale = new Vector3(healthAmount / 50, 0.2f, 1);
        // Debug.Log("Health: " + healthAmount);
    }
}