using UnityEngine;

public class Entity
{
    private GameObject gameObject;
    private GameObject healthBar;
    private Side side;
    private float healthAmount;
    private Rigidbody2D rb;
    private Entity collidedEntity;

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
    
    public void SetCollide(Entity entity)
    {
        collidedEntity = entity;
    }
    
    public bool GetCollide()
    {
        return collidedEntity != null;
    }
    public void TakeDamage(Entity entity)
    {
        healthAmount -= 10;
        healthBar.transform.localScale = new Vector3(healthAmount / 100, 0.15f, 2);
        Debug.Log("Health: " + healthAmount);
    }
}