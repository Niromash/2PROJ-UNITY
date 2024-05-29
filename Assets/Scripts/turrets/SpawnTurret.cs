using UnityEngine;
using System.Collections.Generic;

public class SpawnTurret : MonoBehaviour
{
    public GameManager gameManager;

    
    public void ShowTurrets()
    {
        var playerTeam = gameManager.GetTeams().Find(team => team.GetSide().Equals(Side.Player));
        if (playerTeam != null)
        {
            foreach (var turret in playerTeam.GetTower().GetTurrets())
            {
                turret.GetGameObject().SetActive(true);
            }
        }
    }
}