﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class EvolveAge : MonoBehaviour
{
    public GameManager gameManager;
    private Queue<Age> ages;
    private Age currentAge;

    public void Start()
    {
        ages = new Queue<Age>();
        ages.Enqueue(new MedievalAge());
    }

    public void Evolve()
    {
        Team team = gameManager.GetTeams().Find(team => team.GetSide().Equals(Side.Player));
        Team enemyTeam = gameManager.GetTeams().Find(team => team.GetSide().Equals(Side.Enemy));
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

        Debug.Log("Evolving age for team " + team.GetSide() + " to " + currentAge.GetName());
        team.SetCurrentAge(currentAge);
        team.RemoveExperience(currentAge.GetAgeEvolvingCost());

        ChangeSpellSprite();
        ChangeEntitySprites();

        if (team.GreaterAgeThan(enemyTeam))
        {
            ChangeBackground();
        }

        // If no more ages to evolve, block button and display message and change button sprite
        // and add a new entity button to spawn the last age special entity
    }

    private void ChangeBackground()
    {
        // Todo: change background to new age background for the most advanced team
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
            if (child.gameObject.name.Contains("Button"))
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
}