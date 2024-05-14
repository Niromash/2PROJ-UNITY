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
        entityQueue = new Queue<Entity>();
        turrets = new List<Turret>();
        teams = new List<Team>();
        mainCamera = Camera.main;
        backgroundCanvasGameObject = GameObject.Find("BackgroundCanvas");

        // GameObject turretLeft = GameObject.Find("TurretsLeft");
        // GameObject turretRight = GameObject.Find("TurretsRight");
        //
        // turrets.Add(new Turret(turretLeft, Side.Player));
        // turrets.Add(new Turret(turretRight, Side.Enemy));

        teams.Add(new Team(Side.Player));
        teams.Add(new Team(Side.Enemy));

        // Async task to create a new entity
        StartCoroutine(CreateEntity());
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isSceneLoaded = scene.name == "SampleScene" && mode == LoadSceneMode.Single;
    }

    void Update()
    {
        if (!isSceneLoaded) return;

        // Move the camera using directional keys
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 newCameraPosition = mainCamera.transform.position +
                                    new Vector3(horizontal * Time.deltaTime * 10, mainCamera.velocity.y, 0);

        newCameraPosition.x = Mathf.Clamp(newCameraPosition.x, -1.62f, 23.09f);

        mainCamera.transform.position = newCameraPosition;

        Vector3 newBackgroundPosition = backgroundCanvasGameObject.transform.position +
                                        new Vector3(horizontal * Time.deltaTime * 10,
                                            backgroundCanvasGameObject.transform.position.y, 0);

        newBackgroundPosition.x = Mathf.Clamp(newBackgroundPosition.x, -1.62f, 23.09f);

        backgroundCanvasGameObject.transform.position = newBackgroundPosition;

        MoveEntities();
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

        while (true)
        {
            // Create a new entity
            GameObject baseEntity = Instantiate(prefab, new Vector3(25, 0f, 0), Quaternion.identity);
            baseEntity.SetActive(true);
            AddEntity(new Entity(baseEntity, Side.Enemy));

            // Wait for 10 seconds before creating another entity
            yield return new WaitForSeconds(10);
        }
    }

    public void AddEntity(Entity entity)
    {
        entityQueue.Enqueue(entity);
        teams.Find(team => team.GetSide().Equals(entity.GetSide())).AddEntity(entity);
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
        if (entity.GetCollide())
        {
            return;
        }

        float horizontalMovement = 75.0f;
        if (entity.GetSide() == Side.Enemy)
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
        teams.Find(team => team.GetSide().Equals(entity.GetSide())).RemoveEntity(entity);
        
        foreach (Entity entity1 in entityQueue)
        {
            Debug.Log(entity1);
        }
    }

    public Turret GetTurret(GameObject go)
    {
        foreach (Turret turret in turrets)
        {
            if (turret.GetGameObject() == go)
            {
                return turret;
            }
        }

        return null;
    }
}