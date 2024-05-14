using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<Entity> entities;
    private List<Turret> turrets;

    void Start()
    {
        entities = new List<Entity>();
        turrets = new List<Turret>();

        GameObject turretLeft = GameObject.Find("TurretsLeft");
        GameObject turretRight = GameObject.Find("TurretsRight");
        
        Debug.Log(turretLeft);
        Debug.Log(turretRight);
        
        turrets.Add(new Turret(turretLeft, Side.Player));
        turrets.Add(new Turret(turretRight, Side.Enemy));

        // Async task to create a new entity
        StartCoroutine(CreateEntity());
    }

    void Update()
    {
        StartCoroutine(MoveEntities());

        // Move the camera using directional keys
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 newCameraPosition =
            Camera.main.transform.position + new Vector3(horizontal, vertical, 0) * Time.deltaTime * 10;

        newCameraPosition.y = Mathf.Clamp(newCameraPosition.y, 0.37f, 1.90f);
        newCameraPosition.x = Mathf.Clamp(newCameraPosition.x, -1.62f, 23.09f);

        Camera.main.transform.position = newCameraPosition;

        Vector3 newBackgroundPosition = GameObject.Find("BackgroundCanvas").transform.position +
                                        new Vector3(horizontal, vertical, 0) * Time.deltaTime * 10;

        newBackgroundPosition.y = Mathf.Clamp(newBackgroundPosition.y, 0.37f, 1.90f);
        newBackgroundPosition.x = Mathf.Clamp(newBackgroundPosition.x, -1.62f, 23.09f);

        GameObject.Find("BackgroundCanvas").transform.position = newBackgroundPosition;
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
            GameObject baseEntity = Instantiate(prefab, new Vector3(25, -0.6f, 0), Quaternion.identity);
            baseEntity.SetActive(true);
            AddEntity(new Entity(baseEntity, Side.Enemy, new InfantryStats()));

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

        // tries entities en fonction des collisions avec les adversaires


        // Move the entity
        Rigidbody2D rb = entity.GetRigidbody();
        Vector3 velocity = Vector3.zero;

        float horizontalMovement = 10.0f;
        if (entity.GetSide() == Side.Enemy)
        {
            horizontalMovement *= -1;
        }

        Vector3 targetVelocity = new Vector2(horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);
    }

    public void RemoveEntity(Entity entity)
    {
        entities.Remove(entity);
        Destroy(entity.GetGameObject());
    }

    public Turret GetTurret(GameObject go)
    {
        foreach (Turret turret in turrets)
        {
            if (turret.GetGameObject() == go)
            {
                return turret;
            }
        }

        return null;

    }

}