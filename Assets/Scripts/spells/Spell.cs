using UnityEngine;

public abstract class Spell
{
    private readonly GameObject gameObject;
    private readonly Side side;
    private readonly GameManager gameManager;
    
    protected Spell(string gameObjectName, Side side, GameManager gameManager)
    {
        gameObject = GameObject.Instantiate(GameObject.Find(gameObjectName));
        this.side = side;
        this.gameManager = gameManager;
    }
    
    public GameObject GetGameObject()
    {
        return gameObject;
    }

}