using System.Collections.Generic;
using UnityEngine;
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
        currentAge = ages.Dequeue();

        Debug.Log("Evolving age for team " + team.GetSide() + " to " + currentAge.GetName());
        team.SetCurrentAge(currentAge);
        
        ChangeBackground();
        ChangeEntitySprites();
        ChangeSpellSprite();
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
        background.GetComponent<Renderer>().material.mainTexture = Resources.Load<Texture>("Ages/" + currentAge.GetBackgroundAssetName());
    }

    private void ChangeEntitySprites()
    {
        GameObject entities = GameObject.Find("Entities");
        foreach (Transform child in entities.transform)
        {
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogError("SpriteRenderer not found");
                return;
            }
            spriteRenderer.sprite = Resources.Load<Sprite>("EntitySprites/" + currentAge.GetName() + "/" + child.name.ToLower());
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