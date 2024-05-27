using System;
using System.Reflection;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Spell
{
    private readonly GameObject gameObject;
    private readonly Side side;
    private readonly GameManager gameManager;
    private static SpellStats spellStats;

    protected Spell(string gameObjectName, Side side, SpellStats spellStats, GameManager gameManager)
    {
        gameObject = GameObject.Instantiate(GameObject.Find(gameObjectName));
        this.side = side;
        this.gameManager = gameManager;
        Spell.spellStats = spellStats;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public Side GetSide()
    {
        return side;
    }

    public static SpellStats GetSpellStats()
    {
        return spellStats;
    }

    public abstract void ApplyEffect(Entity entity);
}