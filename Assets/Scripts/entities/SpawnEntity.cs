using UnityEngine;

public class SpawnEntity : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject infantryPrefab;
    public GameObject antiArmorPrefab;
    public Vector2 spawnPosition;

    public void InfantryPlayerSpawn()
    {
        Team playerTeam = gameManager.GetTeams().Find(team => team.GetSide().Equals(Side.Player));
        Spawn(infantryPrefab, playerTeam, new InfantryStats());
    }

    public void AntiArmorPlayerSpawn()
    {
        Team playerTeam = gameManager.GetTeams().Find(team => team.GetSide().Equals(Side.Player));
        Spawn(antiArmorPrefab, playerTeam, new AntiArmorStats());
    }

    private void Spawn(GameObject prefab, Team team, CharacterStats stats)
    {
        if (stats.deploymentCost > team.GetGold())
        {
            Debug.Log("Not enough gold to spawn entity " + prefab.name);
            return;
        }

        team.AddEntity(prefab, stats, spawnPosition);
        team.RemoveGold(stats.deploymentCost);
        Debug.Log("Spawned entity " + stats.name + ". Gold left: " + team.GetGold());
    }
}