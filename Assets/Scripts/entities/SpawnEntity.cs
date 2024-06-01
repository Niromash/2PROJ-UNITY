using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnEntity : MonoBehaviour
{
    public GameManager gameManager;
    public Vector2 spawnPosition;
    private List<EntityAge> entitiesGameObject;
    private int infantryCount;
    private int antiArmorCount;

    public void Start()
    {
        GameObject entities = GameObject.Find("Entities");

        entitiesGameObject = new List<EntityAge>
        {
            new EntityAge(typeof(PrehistoricAge), entities.transform.Find("Prehistoric").gameObject),
            new EntityAge(typeof(MedievalAge), entities.transform.Find("Medieval").gameObject),
        };
    }

    private GameObject GetCurrentEntitiesGameObject(Team team)
    {
        return entitiesGameObject.Find(entity => entity.GetAgeType().Equals(team.GetCurrentAge().GetType()))
            .GetPrefab();
    }

    public void TankPlayerSpawn(Button button)
    {
        Team playerTeam = gameManager.GetTeams().Find(team => team.GetSide().Equals(Side.Player));
        GameObject tankPrefab = GetCurrentEntitiesGameObject(playerTeam).transform.Find("Tank").gameObject;
        Spawn(button, tankPrefab, playerTeam, new TankStats());
    }

    public void InfantryPlayerSpawn(Button button)
    {
        Team playerTeam = gameManager.GetTeams().Find(team => team.GetSide().Equals(Side.Player));
        GameObject infantryPrefab = GetCurrentEntitiesGameObject(playerTeam).transform.Find("Infantry").gameObject;
        Spawn(button, infantryPrefab, playerTeam, new InfantryStats());
    }

    public void AntiArmorPlayerSpawn(Button button)
    {
        Team playerTeam = gameManager.GetTeams().Find(team => team.GetSide().Equals(Side.Player));
        GameObject antiArmorPrefab = GetCurrentEntitiesGameObject(playerTeam).transform.Find("AntiArmor").gameObject;
        Spawn(button, antiArmorPrefab, playerTeam, new AntiArmorStats());
    }

    public void SupportPlayerSpawn(Button button)
    {
        Team playerTeam = gameManager.GetTeams().Find(team => team.GetSide().Equals(Side.Player));
        GameObject supportPrefab = GetCurrentEntitiesGameObject(playerTeam).transform.Find("Support").gameObject;
        Spawn(button, supportPrefab, playerTeam, new SupportStats());
    }

    private void Spawn(Button spawnButton, GameObject prefab, Team team, CharacterStats stats)
    {
        if (team.GetLockedEntityIndex() == spawnButton.transform.GetSiblingIndex())
        {
            Debug.Log("Entity locked, please upgrade to unlock");
            return;
        }

        CharacterStats multipliedStats = stats.GetMultipliedStats(team);
        if (multipliedStats.deploymentCost > team.GetGold())
        {
            Debug.Log("Not enough gold to spawn entity " + prefab.name);
            return;
        }

        string entityName;
        // Increment the counter for the entity type and add it to the name
        if (prefab.name == "Infantry")
        {
            infantryCount++;
            entityName = prefab.name + infantryCount;
        }
        else if (prefab.name == "AntiArmor")
        {
            antiArmorCount++;
            entityName = prefab.name + antiArmorCount;
        }
        else if (prefab.name == "Tank")
        {
            entityName = prefab.name;
        }
        else if (prefab.name == "Support")
        {
            entityName = prefab.name;
        }
        else
        {
            entityName = prefab.name;
        }

        team.AddEntity(prefab, stats, spawnPosition, entityName);
        team.RemoveGold(multipliedStats.deploymentCost);
    }

    private class EntityAge
    {
        private readonly Type ageType;
        private readonly GameObject prefab;

        public EntityAge(Type ageType, GameObject prefab)
        {
            this.ageType = ageType;
            this.prefab = prefab;
        }

        public Type GetAgeType()
        {
            return ageType;
        }

        public GameObject GetPrefab()
        {
            return prefab;
        }
    }
}