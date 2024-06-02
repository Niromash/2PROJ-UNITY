using UnityEngine;
using UnityEngine.UI;

public class UpgradeUnitsButtons : MonoBehaviour
{
    public GameManager gameManager;

    public void UpgradePlayerTank(Button button)
    {
        Upgrade(button, EntityTypes.Tank);
    }

    public void UpgradePlayerInfantry(Button button)
    {
        Upgrade(button, EntityTypes.Infantry);
    }

    public void UpgradePlayerAntiArmor(Button button)
    {
        Upgrade(button, EntityTypes.AntiArmor);
    }

    public void UpgradePlayerSupport(Button button)
    {
        Upgrade(button, EntityTypes.Support);
    }

    public void UpgradePlayerExtraEntity(Button button)
    {
        Upgrade(button, EntityTypes.Extra);
    }

    private void Upgrade(Button spawnButton, EntityTypes entityTypes)
    {
        Team team = gameManager.GetTeams().Find(team => team.GetSide().Equals(Side.Player));
        if (team.GetLockedEntityIndex() == spawnButton.transform.GetSiblingIndex())
        {
            int costToUpgrade = (int)(1500 * team.GetCurrentAge().GetGoldMultiplier());
            if (team.GetGold() < costToUpgrade)
            {
                Debug.Log("Not enough gold to upgrade");
                return;
            }

            team.ToggleLockEntityUi(spawnButton.transform.GetSiblingIndex());
            team.RemoveGold(costToUpgrade);
            return;
        }

        team.GetUpgradeUnits().UpgradeUnit(entityTypes);
    }
}