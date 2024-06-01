using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class SpawnTurret : MonoBehaviour
{
    public GameManager gameManager;
    private int currentTurretIndex = 0;
    private List<Turret> inactiveTurrets = new List<Turret>();


    public void ShowNextTurret()
    {
        if (!GameManager.GetGameState().Equals(GameState.Playing)) return;

        var playerTeam = gameManager.GetTeams().Find(team => team.GetSide().Equals(Side.Player));
        if (playerTeam != null)
        {
            var turrets = playerTeam.GetTower().GetTurrets();
            if (currentTurretIndex < turrets.Count)
            {
                Turret turret = turrets[currentTurretIndex];
                if (turret.GetStats().deploymentCost > playerTeam.GetGold())
                {
                    Debug.Log("Not enough gold to deploy turret");
                    return;
                }

                playerTeam.RemoveGold(turret.GetStats().deploymentCost);
                turret.GetGameObject().SetActive(true);
                turret.MakeActive(playerTeam.GetCurrentAge());
                currentTurretIndex++;
            }
        }
    }

    public void UpgradeTurrets()
    {
        if (!GameManager.GetGameState().Equals(GameState.Playing)) return;

        var playerTeam = gameManager.GetTeams().Find(team => team.GetSide().Equals(Side.Player));
        if (playerTeam != null)
        {
            var turrets = playerTeam.GetTower().GetTurrets();

            // We can upgrade turrets only if the 3 turrets are active
            if (turrets.Where(turret => turret.GetGameObject().activeInHierarchy).Count() != 3)
            {
                return;
            }
            
            if (!playerTeam.GetUpgradeTurrets().CanUpgradeTurrets())
            {
                Debug.Log("Cannot upgrade turrets");
                return;
            }
            
            foreach (var turret in turrets)
            {
                turret.Upgrade();
            }
        }
    }

    private void RemoveTurret(int index)
    {
        if (!GameManager.GetGameState().Equals(GameState.Playing)) return;

        var playerTeam = gameManager.GetTeams().Find(team => team.GetSide().Equals(Side.Player));
        if (playerTeam != null)
        {
            var turrets = playerTeam.GetTower().GetTurrets();
            if (turrets.Count > index)
            {
                var turret = turrets[index];
                turret.GetGameObject().SetActive(false);
                turrets.RemoveAt(index);

                // Refund the player half of the deployment cost
                playerTeam.AddGold(turret.GetStats().deploymentCost / 2);

                inactiveTurrets.Add(turret);
            }
        }
    }
    
    public void RemoveFirstTurret()
    {
       RemoveTurret(0);
    }

    public void RemoveSecondTurret()
    {
       RemoveTurret(1);
    }


    public void RemoveThirdTurret()
    {
       RemoveTurret(2);
    }

    public void ReactivateTurret()
    {
        if (!GameManager.GetGameState().Equals(GameState.Playing)) return;

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