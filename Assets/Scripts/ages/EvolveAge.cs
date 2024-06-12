﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvolveAge : MonoBehaviour
{
    public GameManager gameManager;
    private Queue<Age> ages;
    private Queue<Age> enemyAges;
    private Age currentAge;
    public Button extraEntityButton;
    public Button extraEntityUpgradeButton;

    public static List<Age> allAges = new List<Age>
    {
        new MedievalAge(),
        new ModernAge()
    };

    public EvolveAge()
    {
        ages = new Queue<Age>();
        ages.Enqueue(new MedievalAge());
        ages.Enqueue(new ModernAge());
        enemyAges = new Queue<Age>();
        enemyAges.Enqueue(new MedievalAge());
        enemyAges.Enqueue(new ModernAge());
    }

    public void Evolve()
    {
        if (!GameManager.GetGameState().Equals(GameState.Playing)) return;

        Team team = gameManager.GetTeams().Find(t => t.GetSide().Equals(Side.Player));
        Team enemyTeam = gameManager.GetTeams().Find(t => t.GetSide().Equals(Side.Enemy));
        if (ages.Count == 0)
        {
            Debug.Log("No more ages to evolve");
            return;
        }

        Age lastAge = ages.Peek();
        if (team.GetExperience() < lastAge.GetAgeEvolvingCost())
        {
            Debug.Log("Not enough experience to evolve");
            return;
        }

        currentAge = ages.Dequeue();

        team.SetCurrentAge(currentAge);
        enemyTeam.UpdateTurretPosition(); // Force the enemy team to update its turret position
        team.RemoveExperience(currentAge.GetAgeEvolvingCost());

        // Remove the last locked entity of the team and lock a random entity
        int randomEntityIndexToLock = Random.Range(0, 3);
        team.ToggleLockEntityUi(randomEntityIndexToLock);

        ChangeSpellSprite();
        ChangeUpgradeEntitySprites();
        ChangeEntitySprites();

        if (team.GreaterAgeThan(enemyTeam))
        {
            ChangeBackground();

            // Force all teams to update their turret position according to the most advanced age team
            foreach (Team t in gameManager.GetTeams())
            {
                t.UpdateTurretPosition();
            }
        }

        if (ages.Count == 0)
        {
            // Block change age button
            GameObject button = GameObject.Find("ChangeAge");
            if (button == null)
            {
                Debug.LogError("Button not found");
                return;
            }

            // todo If no more ages to evolve, display message

            GameObject lockPrefab = Resources.Load<GameObject>("Common/lock/Lock");
            GameObject lockObject = Instantiate(lockPrefab, button.transform.position, Quaternion.identity);
            lockObject.name = "Lock";
            lockObject.transform.SetParent(button.transform);
            // Give parent size to lock object
            lockObject.GetComponent<RectTransform>().sizeDelta = button.GetComponent<RectTransform>().sizeDelta;
            button.GetComponent<Button>().interactable = false;

            extraEntityButton.interactable = true;
            extraEntityUpgradeButton.interactable = true;

            Transform child = extraEntityButton.transform.Find("Lock");
            if (child != null)
            {
                Destroy(child.gameObject);
            }

            child = extraEntityUpgradeButton.transform.Find("Lock");
            if (child != null)
            {
                Destroy(child.gameObject);
            }
        }
    }

    private void ChangeBackground()
    {
        GameObject background = GameObject.Find("Quad");
        if (background == null)
        {
            Debug.LogError("Background not found");
            return;
        }

        background.GetComponent<Renderer>().material.mainTexture =
            Resources.Load<Texture>("Ages/" + currentAge.GetBackgroundAssetName());
    }

    private void ChangeEntitySprites()
    {
        GameObject entities = GameObject.Find("SpawnEntities");
        foreach (Transform child in entities.transform)
        {
            Image spriteRenderer = child.gameObject.GetComponent<Image>();
            if (spriteRenderer == null)
            {
                Debug.LogError("SpriteRenderer not found");
                return;
            }

            spriteRenderer.sprite =
                Resources.Load<Sprite>("EntitySprites/" + currentAge.GetName() + "/" + child.name.ToLower());
        }
    }

    private void ChangeUpgradeEntitySprites()
    {
        GameObject entities = CustomGameObjects.FindMaybeDisabledGameObjectByName("Menus", "upgradeUnitsMenu");
        foreach (Transform child in entities.transform)
        {
            Image spriteRenderer = child.gameObject.GetComponent<Image>();
            if (spriteRenderer == null)
            {
                Debug.LogError("SpriteRenderer not found");
                return;
            }

            Sprite sprite =
                Resources.Load<Sprite>("EntitySprites/" + currentAge.GetName() + "/" + child.name.ToLower());
            if (sprite == null)
            {
                Debug.LogError("Sprite not found for EntitySprites/" + currentAge.GetName() + "/" +
                               child.name.ToLower());
                return;
            }

            spriteRenderer.sprite = sprite;
        }
    }

    private void ChangeSpellSprite()
    {
        GameObject button = GameObject.Find("SpawnSpellButton");
        if (button == null)
        {
            Debug.LogError("Button not found");
            return;
        }

        Image spriteRenderer = button.GetComponent<Image>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found");
            return;
        }

        spriteRenderer.sprite = Resources.Load<Sprite>("SpellSprites/" + currentAge.GetName().ToLower() + "/button");
    }

    public void EvolveEnemyAge()
    {
        if (!GameManager.GetGameState().Equals(GameState.Playing)) return;

        Team team = gameManager.GetTeams().Find(t => t.GetSide().Equals(Side.Player));
        Team enemyTeam = gameManager.GetTeams().Find(t => t.GetSide().Equals(Side.Enemy));
        if (enemyAges.Count == 0)
        {
            return;
        }

        Age lastAge = enemyAges.Peek();
        if (enemyTeam.GetExperience() < lastAge.GetAgeEvolvingCost())
        {
            return;
        }

        currentAge = enemyAges.Dequeue();

        enemyTeam.SetCurrentAge(currentAge);
        team.UpdateTurretPosition(); // Force the player team to update its turret position
        enemyTeam.RemoveExperience(currentAge.GetAgeEvolvingCost());

        // Remove the last locked entity of the team and lock a random entity
        int randomEntityIndexToLock = Random.Range(0, 3);
        enemyTeam.ToggleLockEntity(randomEntityIndexToLock);

        if (enemyTeam.GreaterAgeThan(team))
        {
            ChangeBackground();
        }
    }
}