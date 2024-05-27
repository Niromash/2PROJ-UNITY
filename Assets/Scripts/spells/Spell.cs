using UnityEngine;

public abstract class Spell
{
    private readonly GameObject gameObject;
    private readonly Side side;
    private readonly GameManager gameManager;
    private readonly SpellStats spellStats;
    
    protected Spell(string gameObjectName, Side side, SpellStats spellStats, GameManager gameManager)
    {
        gameObject = GameObject.Instantiate(GameObject.Find(gameObjectName));
        this.side = side;
        this.gameManager = gameManager;
        this.spellStats = spellStats;
    }
    
    public GameObject GetGameObject()
    {
        return gameObject;
    }
    
    public Side GetSide()
    {
        return side;
    }
    
    public SpellStats GetSpellStats()
    {
        return spellStats;
    }

    public abstract void ApplyEffect(Entity entity);
}