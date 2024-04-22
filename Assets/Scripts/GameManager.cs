using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<Entity> entities;

    void Start()
    {
        entities = new List<Entity>();

        // Async task to create a new entity
        StartCoroutine(CreateEntity());
    }

    void Update()
    {
        StartCoroutine(MoveEntities());
        
        // Move the camera using directional keys
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Camera.main.transform.position += new Vector3(horizontal, vertical, 0) * Time.deltaTime * 10;
        // Move the background with the camera
        GameObject.Find("BackgroundCanvas").transform.position += new Vector3(horizontal, vertical, 0) * Time.deltaTime * 10;
    }

    private IEnumerator MoveEntities()
    {
        foreach (Entity entity in entities)
        {
            MoveEntity(entity);
        }
        yield return new WaitForSeconds(1);
    }
    
    public IEnumerator CreateEntity()
    {
        // Get the gameObject from the Scene with name "perso1test" in scene "SampleScene"
        GameObject prefab = GameObject.Find("perso1test");
        if (prefab == null)
        {
            Debug.LogError("Prefab not found");
            yield break;
        }

        while (true)
        {
            // Create a new entity
            GameObject baseEntity = Instantiate(prefab, new Vector3(5.5f, -0.6f, 0), Quaternion.identity);
            baseEntity.SetActive(true);
            AddEntity(new Entity(baseEntity, Side.Enemy));

            // Wait for 10 seconds before creating another entity
            yield return new WaitForSeconds(10);
        }
    }

    public void AddEntity(Entity go)
    {
        entities.Add(go);
    }
    
    public Entity GetEntity(GameObject go)
    {
        foreach (Entity entity in entities)
        {
            if (entity.GetGameObject() == go)
            {
                return entity;
            }
        }

        return null;
    }
    
    private void MoveEntity(Entity entity)
    {
        if (entity == null)
        {
            return;
        }

        // Move the entity
        Rigidbody2D rb = entity.GetGameObject().GetComponent<Rigidbody2D>();
        Vector3 velocity = Vector3.zero;
        if (rb.velocity.x < 5.0f)
        {
            float horizontalMovement = 5.0f;
            if (entity.GetSide() == Side.Enemy)
            {
                horizontalMovement *= -1;
            }

            Vector3 targetVelocity = new Vector2(horizontalMovement, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);
        }
    }
    
}