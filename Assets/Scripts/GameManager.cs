using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private List<Turret> turrets;
    private Queue<Entity> entityQueue;
    private List<Team> teams;
    private Camera mainCamera;
    private GameObject backgroundCanvasGameObject;
    private bool isSceneLoaded;
    private static GameState gameState;
    private List<Meteor> meteors;
    private int playerGold;
    private int enemyGold;
    private int playerExperience;
    private int enemyExperience;

    public GameManager()
    {
        entityQueue = new Queue<Entity>();
        turrets = new List<Turret>();
        teams = new List<Team>();
        gameState = GameState.NotStarted;
        meteors = new List<Meteor>();
        playerGold = 0;
        playerExperience = 0;
        enemyGold = 0;
        enemyExperience = 0;
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

        teams.Add(new Team(Side.Player, towerLeft, this));
        teams.Add(new Team(Side.Enemy, towerRight, this));

        gameState = GameState.Playing;

        // Async task to create a new enemy entity
        StartCoroutine(CreateEntity());
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

        newCameraPosition.x = Mathf.Clamp(newCameraPosition.x, 0f, 24.09f);

        mainCamera.transform.position = newCameraPosition;

        Vector3 newBackgroundPosition = backgroundCanvasGameObject.transform.position +
                                        new Vector3(horizontal * Time.deltaTime * 10,
                                            backgroundCanvasGameObject.transform.position.y, 0);

        newBackgroundPosition.x = Mathf.Clamp(newBackgroundPosition.x, 0f, 24.09f);

        backgroundCanvasGameObject.transform.position = newBackgroundPosition;
    }

    private void MoveEntities()
    {
        foreach (Entity entity in entityQueue)
        {
            MoveEntity(entity);
        }
    }

    public IEnumerator CreateEntity()
    {
        GameObject frankiTanki = GameObject.Find("FrankiTanki");
        GameObject marcel = GameObject.Find("Marcel");

        Team enemyTeam = teams.Find(team => team.GetSide().Equals(Side.Enemy));

        int entityCount = 0;
        while (gameState.Equals(GameState.Playing))
        {
            GameObject entityToSpawn = entityCount % 2 == 0 ? frankiTanki : marcel;
            CharacterStats stats = entityCount % 2 == 0 ? new InfantryStats() : new AntiArmorStats();
            // Create a new entity
            GameObject baseEntity = Instantiate(entityToSpawn, new Vector3(25, 0f, 0), Quaternion.identity);
            baseEntity.SetActive(true);
            // remove the tag so that the spawned object is not considered a template
            baseEntity.tag = "Untagged";
            // Flip the entity sprite to face the enemy side
            baseEntity.GetComponent<SpriteRenderer>().flipX = true;
            AddEntity(new Entity(baseEntity, enemyTeam, stats, this));
            entityCount++;

            // Wait for 10 seconds before creating another entity
            yield return new WaitForSeconds(10);
        }
    }

    public void AddEntity(Entity entity)
    {
        entityQueue.Enqueue(entity);
        teams.Find(team => team.GetSide().Equals(entity.GetTeam().GetSide())).AddEntity(entity);
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

    // Using recursion to check if the entity is colliding with next entity, if the next entity is an enemy, then stop moving
    private Entity GetCollidingFrontEnemy(Entity entity)
    {
        Entity forwardEntity = entity.GetCollidedEntityForwards();

        if (forwardEntity == null)
        {
            return null;
        }

        if (!forwardEntity.GetTeam().GetSide().Equals(entity.GetTeam().GetSide()))
        {
            float distance = forwardEntity.GetGameObject().transform.position.x -
                             entity.GetGameObject().transform.position.x;
            if (distance <= entity.GetSpriteRenderer().bounds.size.x)
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
        if (entity.GetCollidedTowerForwards().GetTeam().GetSide().Equals(entity.GetTeam().GetSide())) return true;

        Entity forwardEntity = entity.GetCollidedEntityForwards();
        if (forwardEntity == null) return false;

        return IsCollidingTower(forwardEntity);
    }

    private void MoveEntity(Entity entity)
    {
        if (IsCollidingTower(entity))
        {
            return;
        }

        Entity collidingFrontEnemy = GetCollidingFrontEnemy(entity);
        if (collidingFrontEnemy != null)
        {
            // Check if there is enough space to move
            float distanceToEnemy = Mathf.Abs(collidingFrontEnemy.GetGameObject().transform.position.x -
                                              entity.GetGameObject().transform.position.x);
            if (distanceToEnemy <= entity.GetSpriteRenderer().bounds.size.x)
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
            float distance = entity.GetCollidedEntityForwards().GetGameObject().transform.position.x -
                             entity.GetGameObject().transform.position.x;
            if (distance < entity.GetSpriteRenderer().bounds.size.x)
            {
                return;
            }
        }

        // if the new position is in a tower in front (check with entity rigidbody size), then stop moving
        if (entity.GetCollidedTowerForwards() != null)
        {
            float distance = entity.GetCollidedTowerForwards().GetGameObject().transform.position.x -
                             entity.GetGameObject().transform.position.x;
            if (distance < entity.GetSpriteRenderer().bounds.size.x)
            {
                return;
            }
        }

        entity.GetRigidbody().transform.position += new Vector3(horizontalMovement * Time.deltaTime, 0, 0);
    }

    public void RemoveEntity(Entity entity)
    {
        entityQueue = new Queue<Entity>(entityQueue.Where(s => s != entity));
        teams.Find(team => team.GetSide().Equals(entity.GetTeam().GetSide())).RemoveEntity(entity);
        Destroy(entity.GetGameObject());
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

    public void EndGame()
    {
        gameState = GameState.Finished;
    }

    public List<Team> GetTeams()
    {
        return teams;
    }

    public Meteor GetMeteor(GameObject go)
    {
        return meteors.Find(meteor => meteor.GetGameObject() == go);
    }

    public static GameState GetGameState()
    {
        return gameState;
    }

    public void GainGoldByKill(Team killerTeam, Team killedTeam)
    {
        if (killerTeam != null && killedTeam != null)
        {
            int goldAmount = 25;
            killerTeam.AddGold(goldAmount);
            if (killerTeam.GetSide() == Side.Player)
            {
                playerGold += goldAmount;
            }
            else if (killedTeam.GetSide() == Side.Enemy)
            {
                enemyGold += goldAmount;
            }
        }
    }
    public void GainExpByKill(Team killerTeam, Team killedTeam)
    {
        if (killerTeam != null && killedTeam != null)
        {
            int expAmount = 150;
            killerTeam.AddExperience(expAmount);
            if (killedTeam.GetSide() == Side.Player)
            {
                playerExperience += expAmount;
            }
            else if (killerTeam.GetSide() == Side.Enemy)
            {
                enemyExperience += expAmount;
            }
        }
    }
    public int GetPlayerGold()
    {
        return playerGold;
    }
    public int GetEnemyGold()
    {
        return enemyGold;
    }

    public int GetPlayerExperience()
    {
        return playerExperience;
    }

    public int GetEnemyExperience()
    {
        return enemyExperience;
    }
}