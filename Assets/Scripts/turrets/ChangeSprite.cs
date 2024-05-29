using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite secondTurretSprite;
    public Sprite thirdTurretSprite;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on GameObject: " + gameObject.name);
        }
    }

    public void ChangeToSecondTurret()
    {
        if (secondTurretSprite != null)
        {
            spriteRenderer.sprite = secondTurretSprite;
            spriteRenderer.enabled = false;
            spriteRenderer.enabled = true;
        }
        else
        {
            Debug.LogError("secondTurretSprite is not assigned for: " + gameObject.name);
        }
    }

    public void ChangeToThirdTurret()
    {
        if (thirdTurretSprite != null)
        {
            spriteRenderer.sprite = thirdTurretSprite;
            spriteRenderer.enabled = false;
            spriteRenderer.enabled = true;
        }
        else
        {
            Debug.LogError("thirdTurretSprite is not assigned for: " + gameObject.name);
        }
    }

    public void ChangeSpriteToNextLevel()
    {
        if (spriteRenderer.sprite == null || spriteRenderer.sprite.name == "tourelle")
        {
            ChangeToSecondTurret();
        }
        else if (spriteRenderer.sprite == secondTurretSprite)
        {
            ChangeToThirdTurret();
        }
    }
}