using UnityEngine;
using UnityEngine.Serialization;

public class RemoveSubMenu : MonoBehaviour
{
    public Canvas SubMenu;
    private bool isMainOpen = false;
    
    void Start()
    {
        SubMenu.gameObject.SetActive(false);
    }
    
    public void OpenSubMenu()
    {
        if (isMainOpen)
        {
            CloseSubMenu();
        }
        else
        {
            OpenMenu();
        }
    }
    
    void OpenMenu()
    {
        isMainOpen = true;
        SubMenu.gameObject.SetActive(true);
    }
    
    public void CloseSubMenu()
    {
        isMainOpen = false;
        SubMenu.gameObject.SetActive(false);
    }
}