using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUnits : MonoBehaviour
{
    public GameManager gameManager;
    private Dictionary<string, UnitUpgrade> unitUpgrades;
    private Dictionary<string, int> unitLevels;

    void Start()
    {
        unitUpgrades = new Dictionary<string, UnitUpgrade>
        {
            { "Tank", new TankUpgrade() },
            { "Infantry", new InfantryUpgrade() },
            { "Support", new SupportUpgrade() },
            { "Anti-Armor", new AntiArmorUpgrade() }
        };

        unitLevels = new Dictionary<string, int>
        {
            { "Tank", 0 },
            { "Infantry", 0 },
            { "Support", 0 },
            { "Anti-Armor", 0 }
        };
    }

    public void UpgradeUnit(string unitType)
    {
        if (!unitUpgrades.ContainsKey(unitType)) return;

        UnitUpgrade upgrade = unitUpgrades[unitType];
        Team team = gameManager.GetTeams().Find(t => t.GetSide().Equals(Side.Player));

        if (team.GetGold() < upgrade.GetUpgradeCost())
        {
            Debug.Log("Not enough gold to upgrade " + unitType);
            return;
        }

        team.RemoveGold(upgrade.GetUpgradeCost());
        ApplyUpgrade(upgrade, team);

        unitLevels[unitType]++; // Increment the level of the upgraded unit
        Debug.Log(unitType + " upgraded to level " + unitLevels[unitType]);
    }

    private void ApplyUpgrade(UnitUpgrade upgrade, Team team)
    {
        
    }
}