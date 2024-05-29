using UnityEngine;

public class EntityToSpawn
{
    private readonly GameObject prefab;
    private readonly Team team;
    private readonly CharacterStats stats;
    private readonly Vector3 spawnPosition;

    public EntityToSpawn(GameObject prefab, Team team, CharacterStats stats, Vector3 spawnPosition)
    {
        this.prefab = prefab;
        this.team = team;
        this.stats = stats;
        this.spawnPosition = spawnPosition;
    }
    
    public GameObject GetPrefab()
    {
        return prefab;
    }
    
    public Team GetTeam()
    {
        return team;
    }
    
    public CharacterStats GetStats()
    {
        return stats;
    }
    
    public Vector3 GetSpawnPosition()
    {
        return spawnPosition;
    }
}