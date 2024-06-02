using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private List<Team> teams;
    private Camera mainCamera;
    private GameObject backgroundCanvasGameObject;
    private bool isSceneLoaded;
    private static GameState gameState;
    private List<Spell> spells;
    private readonly EntityStrengthWeakness entityStrengthWeakness;
    private static float audioVolume;
    private Team mostAdvancedAgeTeam;

    public GameManager()
    {
        teams = new List<Team>();
        gameState = GameState.NotStarted;
        spells = new List<Spell>();
        entityStrengthWeakness = new EntityStrengthWeakness();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        mainCamera = Camera.main;
        backgroundCanvasGameObject = GameObject.Find("BackgroundCanvas");

        GameObject towerLeft = GameObject.Find("TowerLeft");
        GameObject towerRight = GameObject.Find("TowerRight");

        if (!isSceneLoaded) return;

        teams.Add(new Team(Side.Player, towerLeft, this));
        teams.Add(new Team(Side.Enemy, towerRight, this));

        gameState = GameState.Playing;

        // Async task to create a new enemy entity
        StartCoroutine(CreateEntity());

        foreach (Team team in teams)
        {
            StartCoroutine(team.SpawnEntities());
            StartCoroutine(team.GainPassiveGolds());
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isSceneLoaded = scene.name == "SampleScene" && mode == LoadSceneMode.Single;
    }

    void Update()
    {
        if (!isSceneLoaded) return;
        HandleCameras();

        if (!gameState.Equals(GameState.Playing)) return;
        MoveEntities();
    }

    private void HandleCameras()
    {
        // Move the camera using directional keys
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 newCameraPosition = mainCamera.transform.position +
                                    new Vector3(horizontal * Time.deltaTime * 10, mainCamera.velocity.y, 0);

        newCameraPosition.x = Mathf.Clamp(newCameraPosition.x, -9.38f, 35f);

        mainCamera.transform.position = newCameraPosition;

        Vector3 newBackgroundPosition = backgroundCanvasGameObject.transform.position +
                                        new Vector3(horizontal * Time.deltaTime * 10,
                                            backgroundCanvasGameObject.transform.position.y, 0);

        newBackgroundPosition.x = Mathf.Clamp(newBackgroundPosition.x, -9.38f, 35f);

        backgroundCanvasGameObject.transform.position = newBackgroundPosition;
    }

    private void MoveEntities()
    {
        List<Entity> entities = teams.SelectMany(team => team.GetEntities()).ToList();
        foreach (Entity entity in entities)
        {
            MoveEntity(entity);
        }
    }

    public IEnumerator CreateEntity()
    {
        GameObject frankiTanki = GameObject.Find("Tank");
        GameObject marcel = GameObject.Find("Infantry");

        Team enemyTeam = teams.Find(team => team.GetSide().Equals(Side.Enemy));

        int entityCount = 0;
        while (gameState.Equals(GameState.Playing))
        {
            CharacterStats stats = entityCount % 2 == 0 ? new TankStats() : new InfantryStats();
            CharacterStats multipliedStats = stats.GetMultipliedStats(enemyTeam);

            if (multipliedStats.deploymentCost > enemyTeam.GetGold())
            {
                Debug.Log("Not enough gold to spawn enemy entity " + stats.GetName());
            }
            else
            {
                GameObject entityToSpawn = entityCount % 2 == 0 ? frankiTanki : marcel;
                if (enemyTeam.AddEntity(entityToSpawn, stats, new Vector3(37, 0f, 0), stats.GetName()))
                {
                    enemyTeam.RemoveGold(multipliedStats.deploymentCost);
                    entityCount++;
                }
            }

            // Wait for 10 seconds before creating another entity
            yield return new WaitForSeconds(2);
        }
    }

    public Entity GetEntity(GameObject go)
    {
        foreach (Team team in teams)
        {
            Entity entityFound = team.GetEntities().Find(entity => entity.GetGameObject() == go);
            if (entityFound != null)
            {
                return entityFound;
            }
        }

        return null;
    }

    public Turret GetTurret(GameObject go)
    {
        foreach (Team team in teams)
        {
            Turret turretFound = team.GetTower().GetTurrets().Find(turret => turret.GetGameObject() == go);
            if (turretFound != null)
            {
                return turretFound;
            }
        }

        return null;
    }


    // Using recursion to check if the entity is colliding with next entity, if the next entity is an enemy, then stop moving
    private Entity GetCollidingFrontEnemy(Entity entity)
    {
        Entity forwardEntity = entity.GetCollidedEntityForwards();
        if (forwardEntity == null || forwardEntity.GetGameObject() == null) return null;

        if (!forwardEntity.GetTeam().GetSide().Equals(entity.GetTeam().GetSide()))
        {
            float distance = forwardEntity.GetGameObject().transform.position.x -
                             entity.GetGameObject().transform.position.x;
            if (distance <= entity.GetSize().x)
            {
                return forwardEntity;
            }
        }

        return GetCollidingFrontEnemy(forwardEntity);
    }

    // IsCollidingTower returns using recursive calls, if all the entity from the team are colliding with a forward tower, if one of the entity is not colliding, then return false
    private bool IsCollidingTower(Entity entity)
    {
        if (entity.GetCollidedTowerForwards() == null) return false;
        if (!entity.GetCollidedTowerForwards().GetTeam().GetSide().Equals(entity.GetTeam().GetSide())) return true;

        Entity forwardEntity = entity.GetCollidedEntityForwards();
        if (forwardEntity == null) return false;

        return IsCollidingTower(forwardEntity);
    }

    private void MoveEntity(Entity entity)
    {
        if (IsCollidingTower(entity)) return;

        Entity collidingFrontEnemy = GetCollidingFrontEnemy(entity);
        if (collidingFrontEnemy != null)
        {
            // Check if there is enough space to move
            float distanceToEnemy = Mathf.Abs(collidingFrontEnemy.GetGameObject().transform.position.x -
                                              entity.GetGameObject().transform.position.x);
            if (distanceToEnemy <= entity.GetSize().x)
            {
                return;
            }
        }

        float moveSpeed = entity.GetStats().blockPerSecondMovementSpeed;

        // if the colliding entity is an ally, apply the collided entity move speed to the current entity if the current entity is faster than the collided entity
        Entity collidedAlly = entity.GetCollidedEntityForwards();
        if (collidedAlly != null && collidedAlly.GetTeam().GetSide().Equals(entity.GetTeam().GetSide()) &&
            collidedAlly.GetStats().blockPerSecondMovementSpeed < entity.GetStats().blockPerSecondMovementSpeed)
        {
            moveSpeed = collidedAlly.GetStats().blockPerSecondMovementSpeed;
        }

        float horizontalMovement = 10 * moveSpeed;
        if (entity.GetTeam().GetSide() == Side.Enemy)
        {
            horizontalMovement *= -1;
        }

        // if the new position is in an entity in front (check with entity rigidbody size), then stop moving
        if (entity.GetCollidedEntityForwards() != null)
        {
            if (entity.GetCollidedEntityForwards().GetGameObject() == null) return;

            float distance;
            if (entity.GetTeam().GetSide().Equals(Side.Player))
            {
                distance = entity.GetCollidedEntityForwards().GetGameObject().transform.position.x -
                           entity.GetGameObject().transform.position.x;
            }
            else
            {
                distance = entity.GetGameObject().transform.position.x -
                           entity.GetCollidedEntityForwards().GetGameObject().transform.position.x;
            }

            if (distance < entity.GetSize().x)
            {
                return;
            }
        }

        // if the new position is in a tower in front (check with entity rigidbody size), then stop moving
        if (entity.GetCollidedTowerForwards() != null)
        {
            float distance = entity.GetCollidedTowerForwards().GetGameObject().transform.position.x -
                             entity.GetGameObject().transform.position.x;
            if (distance < entity.GetSize().x)
            {
                return;
            }
        }

        entity.GetRigidbody().transform.position += new Vector3(horizontalMovement * Time.deltaTime, 0, 0);
    }

    public Tower GetTower(GameObject go)
    {
        Team team = teams.Find(team => team.GetTower().GetGameObject() == go);
        if (team == null)
        {
            return null;
        }

        return team.GetTower();
    }

    public void EndGame(Damager damager)
    {
        GameObject gameObjectByName = CustomGameObjects.FindMaybeDisabledGameObjectByName("Menus", "EndGameScreen");
        gameObjectByName.SetActive(true);

        GameObject winnerGameObject = GameObject.Find("WinnerText");
        TextMeshProUGUI winnerText = winnerGameObject.GetComponent<TextMeshProUGUI>();
        winnerText.text = damager.GetTeam().GetSide().ToString();

        GameObject killedByGameObject = GameObject.Find("KilledByText");
        TextMeshProUGUI killedByText = killedByGameObject.GetComponent<TextMeshProUGUI>();
        killedByText.text = damager.GetName();

        Debug.Log("Game Over! " + damager.GetTeam().GetSide() + " team won, killed by " + damager.GetName());
        gameState = GameState.Finished;
    }

    [Serializable]
    private class CharacterData
    {
        public string winner;
    }


    public List<Team> GetTeams()
    {
        return teams;
    }

    public Spell GetSpell(GameObject go)
    {
        return spells.Find(spell => spell.GetGameObject() == go);
    }

    public void AddSpell(Spell spell)
    {
        spells.Add(spell);
    }

    public void RemoveSpell(Spell spell)
    {
        spells = new List<Spell>(spells.Where(s => s != spell));
        Destroy(spell.GetGameObject());
    }

    public static GameState GetGameState()
    {
        return gameState;
    }

    public static void SetGameState(GameState state)
    {
        gameState = state;
    }

    public EntityStrengthWeakness GetEntityStrengthWeakness()
    {
        return entityStrengthWeakness;
    }

    public static void SetAudioVolume(float volume)
    {
        audioVolume = volume;
    }

    public static float GetAudioVolume()
    {
        return audioVolume;
    }

    public void SetMostAdvancedAgeTeam(Team team)
    {
        mostAdvancedAgeTeam = team;
    }

    public Team GetMostAdvancedAgeTeam()
    {
        return mostAdvancedAgeTeam;
    }
}