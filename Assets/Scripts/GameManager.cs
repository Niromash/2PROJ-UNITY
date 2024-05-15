using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private GameState gameState;

    public GameManager()
    {
        entityQueue = new Queue<Entity>();
        turrets = new List<Turret>();
        teams = new List<Team>();
        gameState = GameState.NotStarted;
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

        GameObject turretLeft = GameObject.Find("TurretsLeft");
        GameObject turretRight = GameObject.Find("TurretsRight");
        
        teams.Add(new Team(Side.Player, turretLeft, this));
        teams.Add(new Team(Side.Enemy, turretRight, this));
        
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
        // Get the gameObject from the Scene with name "perso1test" in scene "SampleScene"
        GameObject prefab = GameObject.Find("perso1test");
        if (prefab == null)
        {
            Debug.LogError("Prefab not found");
            yield break;
        }
        
        Team enemyTeam = teams.Find(team => team.GetSide().Equals(Side.Enemy)); 

        while (gameState.Equals(GameState.Playing))
        {
            // Create a new entity
            GameObject baseEntity = Instantiate(prefab, new Vector3(25, 0f, 0), Quaternion.identity);
            baseEntity.SetActive(true);
            AddEntity(new Entity(baseEntity, enemyTeam, new InfantryStats(), this));

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

    private void MoveEntity(Entity entity)
    {
        if (entity.IsKilled()) return;
        if (entity.IsForewardColliding()) return;

        float horizontalMovement = 75.0f;
        if (entity.GetTeam().GetSide() == Side.Enemy)
        {
            horizontalMovement *= -1;
        }

        Rigidbody2D rb = entity.GetRigidbody();
        Vector3 moveTowards = Vector3.MoveTowards(rb.position,
            new Vector2(rb.position.x + horizontalMovement * Time.deltaTime, rb.position.y), 0.1f);
        rb.MovePosition(moveTowards);
    }

    public void RemoveEntity(Entity entity)
    {
        entityQueue = new Queue<Entity>(entityQueue.Where(s => s != entity));
        teams.Find(team => team.GetSide().Equals(entity.GetTeam().GetSide())).RemoveEntity(entity);
        Destroy(entity.GetGameObject());
    }

    public Tower GetTower(GameObject go)
    {
        return teams.Find(team => team.GetTower().GetGameObject() == go).GetTower();
    }

    public void EndGame()
    {
        gameState = GameState.Finished;
    }
    
    public List<Team> GetTeams()
    {
        return teams;
    }
}