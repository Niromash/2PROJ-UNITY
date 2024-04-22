using UnityEngine;

public class Entity
{
    private GameObject gameObject;
    private Side side;

    public Entity(GameObject go, Side side)
    {
        gameObject = go;
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