using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<GameObject> entities;
    private Vector3 velocity = Vector3.zero;
    
    void Start()
    {
        entities = new List<GameObject>();
    }

    void Update()
    {
        entities.ForEach(entity =>
        {
            MoveEntity(entity);
        });
        
    }

    
    public void AddEntity(GameObject go)
    {
        entities.Add(go);
    }

    
    private void MoveEntity(GameObject entity)
    {
        // Déplacez l'objet créé continuellement vers la droite jusqu'à ce qu'il entre en collision avec quelque chose
        Rigidbody2D rb = entity.GetComponent<Rigidbody2D>();
        if (entity != null && rb.velocity.x < 5.0f)
        {
            float horizontalMovement = 5.0f * 2500 * Time.deltaTime; // Ajustez la vitesse selon les besoins
            Vector3 targetVelocity = new Vector2(horizontalMovement, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);
        }
    }
    
}