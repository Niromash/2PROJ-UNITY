using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Turret
{
    private GameObject gameObject;
    private GameManager gameManager;
    private TilemapCollider2D tilemapCollider;
    private Side side;
    private int life;
    
    public Turret(GameObject go, Side side)
    {
        gameObject = go;
        tilemapCollider = go.GetComponent<TilemapCollider2D>();
        
        life = 1000;
        this.side = side;
    }

    
    public GameObject GetGameObject()
    {
        return gameObject;
    }
    
    public Side GetSide()
    {
        return side;
    }
}
