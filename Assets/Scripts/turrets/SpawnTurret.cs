using UnityEngine;


public class SpawnTurret : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject bulletPrefab;
    public Entity player;
    private int currentTurretIndex = 0;

    // Ajoutez cette fonction pour démarrer le spawn des projectiles
    void Start()
    {
        InvokeRepeating("SpawnBullet", 5.0f, 5.0f);
    }

    public void ShowNextTurret()
    {
        var playerTeam = gameManager.GetTeams().Find(team => team.GetSide().Equals(Side.Player));
        if (playerTeam != null)
        {
            var turrets = playerTeam.GetTower().GetTurrets();
            if (currentTurretIndex < turrets.Count)
            {
                turrets[currentTurretIndex].GetGameObject().SetActive(true);
                currentTurretIndex++;
            }
        }
    }

    public void UpgradeTurrets()
    {
        var playerTeam = gameManager.GetTeams().Find(team => team.GetSide().Equals(Side.Player));
        if (playerTeam != null)
        {
            Debug.Log(playerTeam.GetTower().GetTurrets());
            var turrets = playerTeam.GetTower().GetTurrets();
            foreach (var turret in turrets)
            {
                turret.GetStats().Upgrade(1.2f);
                Debug.Log(playerTeam.GetTower().GetTurrets());
            }
        }
    }
}