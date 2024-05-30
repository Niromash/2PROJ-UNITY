using UnityEngine;

public abstract class Spell : Damager
{
    private readonly GameObject gameObject;
    private readonly Team team;
    private static SpellStats spellStats;

    protected Spell(string gameObjectName, Team team, SpellStats spellStats)
    {
        gameObject = GameObject.Instantiate(GameObject.Find(gameObjectName));
        this.team = team;
        Spell.spellStats = spellStats;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public Team GetTeam()
    {
        return team;
    }

    public static SpellStats GetSpellStats()
    {
        return spellStats;
    }

    public DamagerStats GetDamagerStats()
    {
        return spellStats;
    }

    public string GetName()
    {
        return spellStats.GetName();
    }

    public abstract void ApplyEffect(Entity entity);
}