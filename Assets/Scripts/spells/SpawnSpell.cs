using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnSpell : MonoBehaviour
{
    public GameManager gameManager;

    public void SpawnPlayerAge()
    {
        Team team = gameManager.GetTeams().Find(team => team.GetSide().Equals(Side.Player));
        Spawn(team, team.GetCurrentAge().GetSpellType(), team.GetCurrentAge().GetSpellStats());
    }

    private void Spawn(Team team, Type spellType, SpellStats spellStats)
    {
        if (!GameManager.GetGameState().Equals(GameState.Playing)) return;
        team.CastSpells(spellType, spellStats);
    }
}