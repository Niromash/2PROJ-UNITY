using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class SpawnTurret : MonoBehaviour
{
    public GameManager gameManager;
    private int currentTurretIndex = 0;

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

                // Check if any active turret has a higher level than the turret to be deployed
                var activeTurrets = turrets.Where(t => t.GetGameObject().activeInHierarchy);
                if (activeTurrets.Any(t => t.GetLevel() > turret.GetLevel()))
                {
                    Debug.Log("Cannot deploy a turret with a lower level than any active turret");
                    return;
                }

                SpriteRenderer spriteRenderer = turret.GetGameObject().gameObject.GetComponent<SpriteRenderer>();
                if (spriteRenderer == null)
                {
                    Debug.LogError("SpriteRenderer not found");
                    return;
                }

                string basePath = "Turrets/" + playerTeam.GetCurrentAge().GetName().ToLower() + "/";

                spriteRenderer.sprite =
                    Resources.Load<Sprite>(basePath + (turret.GetLevel() + 1));

                // Update changesprite script
                ChangeSprite changeSprite = turret.GetGameObject().gameObject.GetComponent<ChangeSprite>();
                changeSprite.firstTurretSprite = Resources.Load<Sprite>(basePath + "1");
                changeSprite.secondTurretSprite = Resources.Load<Sprite>(basePath + "2");
                changeSprite.thirdTurretSprite = Resources.Load<Sprite>(basePath + "3");

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

            // We can upgrade turrets only if the 4 turrets are active
            if (turrets.Where(turret => turret.GetGameObject().activeInHierarchy).Count() != 4)
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

    public void RemoveTurret()
    {
        if (!GameManager.GetGameState().Equals(GameState.Playing)) return;

        var playerTeam = gameManager.GetTeams().Find(team => team.GetSide().Equals(Side.Player));
        if (playerTeam != null)
        {
            var turrets = playerTeam.GetTower().GetTurrets();
            // Remove the last active turret
            for (int i = turrets.Count - 1; i >= 0; i--)
            {
                if (turrets[i].GetGameObject().activeInHierarchy)
                {
                    turrets[i].GetGameObject().SetActive(false);
                    // Refund the player half of the deployment cost
                    playerTeam.AddGold(turrets[i].GetStats().deploymentCost / 2);
                    turrets[i].ResetLevel();
                    turrets[i].ResetSprite();
                    turrets[i].ResetStats();

                    currentTurretIndex--;
                    break;
                }
            }
        }
    }
}