using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class SpawnTurret : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject bulletPrefab;
    public Entity player;
    private int currentTurretIndex = 0;
    private List<Turret> inactiveTurrets = new List<Turret>(); // Ajout de la liste inactiveTurrets


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
            var turrets = playerTeam.GetTower().GetTurrets();

            // Vérifiez que les trois tourelles sont actives
            if (turrets.Where(turret => turret.GetGameObject().activeInHierarchy).Count() != 3)
            {
                return;
            }

            foreach (var turret in turrets)
            {
                if (turret.CanUpgrade())
                {
                    turret.Upgrade(1.2f);
                    var changeSprite = turret.GetGameObject().GetComponent<ChangeSprite>();
                    if (changeSprite != null)
                    {
                        changeSprite.ChangeSpriteToNextLevel();
                    }
                }
            }
        }
    }
    
    public void RemoveFirstTurret()
    {
        var playerTeam = gameManager.GetTeams().Find(team => team.GetSide().Equals(Side.Player));
        if (playerTeam != null)
        {
            var turrets = playerTeam.GetTower().GetTurrets();
            if (turrets.Count > 0)
            {
                var turret = turrets[0];
                turret.GetGameObject().SetActive(false);
                turrets.RemoveAt(0);
                inactiveTurrets.Add(turret);
            }
        }
    }

    public void RemoveSecondTurret()
    {
        var playerTeam = gameManager.GetTeams().Find(team => team.GetSide().Equals(Side.Player));
        if (playerTeam != null)
        {
            var turrets = playerTeam.GetTower().GetTurrets();
            if (turrets.Count > 1)
            {
                var turret = turrets[1];
                turret.GetGameObject().SetActive(false);
                turrets.RemoveAt(1);
                inactiveTurrets.Add(turret);
            }
        }
    }
    

    public void RemoveThirdTurret()
    {
        var playerTeam = gameManager.GetTeams().Find(team => team.GetSide().Equals(Side.Player));
        if (playerTeam != null)
        {
            var turrets = playerTeam.GetTower().GetTurrets();
            if (turrets.Count > 2)
            {
                var turret = turrets[2];
                turret.GetGameObject().SetActive(false);
                turrets.RemoveAt(2);
                inactiveTurrets.Add(turret);
            }
        }
    }
    
    public void ReactivateTurret()
    {
        if (inactiveTurrets.Count > 0)
        {
            var turret = inactiveTurrets[0];
            turret.GetGameObject().SetActive(true);
            inactiveTurrets.RemoveAt(0);
            // Ajoutez la tourelle à la liste des tourelles actives
            var playerTeam = gameManager.GetTeams().Find(team => team.GetSide().Equals(Side.Player));
            if (playerTeam != null)
            {
                var turrets = playerTeam.GetTower().GetTurrets();
                turrets.Add(turret);
            }
        }
    }

    
    
    
}