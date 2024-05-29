using UnityEngine;

public class UpgradeTurretMenu : MonoBehaviour
{
    public Canvas upgradeTurretCanvas;
    private bool isOpen = false;
    
    void Start()
    {
        upgradeTurretCanvas.gameObject.SetActive(false);
    }
    
    public void OpenUpgradeTurretMenu()
    {
        if (isOpen)
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
        isOpen = true;
        upgradeTurretCanvas.gameObject.SetActive(true);
    }
    
    public void CloseTurretMenu()
    {
        isOpen = false;
        upgradeTurretCanvas.gameObject.SetActive(false);
    }
    
}