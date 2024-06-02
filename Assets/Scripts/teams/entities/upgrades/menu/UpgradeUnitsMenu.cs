using UnityEngine;

public class UpgradeUnitsMenu : MonoBehaviour
{
    public Canvas upgradeUnitsCanvas;
    private bool isMainOpen;

    void Start()
    {
        upgradeUnitsCanvas.gameObject.SetActive(false);
    }

    public void OpenUpgradeMenu()
    {
        if (isMainOpen)
        {
            CloseUpgradeUnitsMenu();
        }
        else
        {
            OpenUpgradeUnitsMenu();
        }
    }

    private void OpenUpgradeUnitsMenu()
    {
        isMainOpen = true;
        upgradeUnitsCanvas.gameObject.SetActive(true);
    }

    private void CloseUpgradeUnitsMenu()
    {
        isMainOpen = false;
        upgradeUnitsCanvas.gameObject.SetActive(false);
    }
}