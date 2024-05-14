using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Entity
{
    private GameObject gameObject;
    private GameObject healthBar;
    private Side side;
    private Rigidbody2D rb;
    private Entity collidedEntity;
    private GameManager gameManager;
    
    private CharacterStats stats;

    public Entity(GameObject go, Side side, CharacterStats stats)
    {
        rb = go.GetComponent<Rigidbody2D>();
        
        this.stats = stats;
        
        gameObject = go;
        
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
    
    public CharacterStats GetStats()
    {
        return stats;
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
        Debug.Log("healthAmount: " + entity.GetStats().Health);
        entity.GetStats().Health -= entity.GetStats().DamagePerSecond;
        healthBar.transform.localScale = new Vector3(GetStats().Health / 50, 0.2f, 1);
    }  
    
    public void Kill()
    {
        gameManager.RemoveEntity(this);
        Object.Destroy(this.GetGameObject());
    }
}