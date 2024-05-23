using UnityEngine;
public class Meteor
{
    private GameObject gameObject;
    private Side side;
    private GameManager gameManager;

    public Meteor(GameObject gameObject, Side side, GameManager gameManager)
    {
        this.gameObject = gameObject;
        this.side = side;
        this.gameManager = gameManager;
    }
    
    public GameObject GetGameObject()
    {
        return gameObject;
    }

}