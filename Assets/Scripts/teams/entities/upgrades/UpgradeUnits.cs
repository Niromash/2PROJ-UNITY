using System.Collections.Generic;
using UnityEngine;

public class UpgradeUnits
{
    private readonly Team team;
    private Dictionary<EntityTypes, Queue<UnitUpgrade>> unitUpgrades;
    private Dictionary<EntityTypes, UnitUpgrade> currentUnitLevels;

    public UpgradeUnits(Team team)
    {
        this.team = team;

        Queue<UnitUpgrade> tankUpgrades = new Queue<UnitUpgrade>();
        tankUpgrades.Enqueue(new TankUpgrade1());
        tankUpgrades.Enqueue(new TankUpgrade2());
        tankUpgrades.Enqueue(new TankUpgrade3());
        Queue<UnitUpgrade> infantryUpgrades = new Queue<UnitUpgrade>();
        infantryUpgrades.Enqueue(new InfantryUpgrade1());
        infantryUpgrades.Enqueue(new InfantryUpgrade2());
        infantryUpgrades.Enqueue(new InfantryUpgrade3());
        Queue<UnitUpgrade> supportUpgrades = new Queue<UnitUpgrade>();
        supportUpgrades.Enqueue(new SupportUpgrade1());
        supportUpgrades.Enqueue(new SupportUpgrade2());
        supportUpgrades.Enqueue(new SupportUpgrade3());
        Queue<UnitUpgrade> antiArmorUpgrades = new Queue<UnitUpgrade>();
        antiArmorUpgrades.Enqueue(new AntiArmorUpgrade1());
        antiArmorUpgrades.Enqueue(new AntiArmorUpgrade2());
        antiArmorUpgrades.Enqueue(new AntiArmorUpgrade3());     
        Queue<UnitUpgrade> extraUpgrades = new Queue<UnitUpgrade>();
        extraUpgrades.Enqueue(new AntiArmorUpgrade1());
        extraUpgrades.Enqueue(new AntiArmorUpgrade2());
        extraUpgrades.Enqueue(new AntiArmorUpgrade3());

        unitUpgrades = new Dictionary<EntityTypes, Queue<UnitUpgrade>>
        {
            { EntityTypes.Tank, tankUpgrades },
            { EntityTypes.Infantry, infantryUpgrades },
            { EntityTypes.Support, supportUpgrades },
            { EntityTypes.AntiArmor, antiArmorUpgrades },
            { EntityTypes.Extra, extraUpgrades }
        };

        currentUnitLevels = new Dictionary<EntityTypes, UnitUpgrade>
        {
            { EntityTypes.Tank, null },
            { EntityTypes.Infantry, null },
            { EntityTypes.Support, null },
            { EntityTypes.AntiArmor, null },
            { EntityTypes.Extra, null }
        };
    }

    public void UpgradeUnit(EntityTypes unityName)
    {
        if (!unitUpgrades.ContainsKey(unityName)) return;

        Queue<UnitUpgrade> upgrades = unitUpgrades[unityName];
        if (upgrades.Count == 0)
        {
            Debug.Log("No more upgrades for " + unityName);
            return;
        }

        UnitUpgrade nextUpgrade = upgrades.Peek();

        if (team.GetGold() < nextUpgrade.GetUpgradeCost())
        {
            Debug.Log("Not enough gold to upgrade " + unityName);
            return;
        }

        team.RemoveGold(nextUpgrade.GetUpgradeCost());

        ApplyUpgrade(nextUpgrade.GetName());
    }

    private void ApplyUpgrade(EntityTypes unitName)
    {
        UnitUpgrade nextUpgrade = unitUpgrades[unitName].Dequeue();

        // Upgrade current entity level
        currentUnitLevels[nextUpgrade.GetName()] = nextUpgrade;
        Debug.Log(nextUpgrade.GetName() + " upgraded to level " + nextUpgrade.GetUpgradeLevel());
    }

    public UnitUpgrade GetUnitUpgrade(EntityTypes unitName)
    {
        return currentUnitLevels[unitName];
    }
}