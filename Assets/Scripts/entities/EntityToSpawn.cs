using UnityEngine;

public class EntityToSpawn
{
    private readonly GameObject prefab;
    private readonly Team team;
    private readonly CharacterStats stats;
    private readonly Vector3 spawnPosition;
    private readonly string entityName;

    public EntityToSpawn(GameObject prefab, Team team, CharacterStats stats, Vector3 spawnPosition, string entityName)
    {
        this.prefab = prefab;
        this.team = team;
        this.stats = stats;
        this.spawnPosition = spawnPosition;
        this.entityName = entityName;
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
    
    public string GetEntityName()
    {
        return entityName;
    }
}