using UnityEngine;

public class UpgradeTurretMenu : MonoBehaviour
{
    public Canvas upgradeTurretCanvas;
    private bool isMainOpen = false;

    void Start()
    {
        upgradeTurretCanvas.gameObject.SetActive(false);
    }

    public void OpenUpgradeTurretMenu()
    {
        if (isMainOpen)
        {
            CloseTurretMenu();
        }
        else
        {
            OpenTurretMenu();
        }
    }

    void OpenTurretMenu()
    {
        isMainOpen = true;
        upgradeTurretCanvas.gameObject.SetActive(true);
    }

    public void CloseTurretMenu()
    {
        isMainOpen = false;
        upgradeTurretCanvas.gameObject.SetActive(false);
    }
}