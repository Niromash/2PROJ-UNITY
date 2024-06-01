using System.Collections.Generic;
using UnityEngine;

public class UpgradeTurrets
{
    private readonly Team team;
    private Dictionary<int, Queue<TurretUpgrade>> turretUpgrades;
    private Dictionary<int, TurretUpgrade> currentTurretLevels;

    public UpgradeTurrets(Team team)
    {
        this.team = team;

        Queue<TurretUpgrade> turret1Upgrades = new Queue<TurretUpgrade>();
        turret1Upgrades.Enqueue(new TurretUpgrade1());
        turret1Upgrades.Enqueue(new TurretUpgrade2());

        // Copy turret1Upgrades
        Queue<TurretUpgrade> turret2Upgrades = new Queue<TurretUpgrade>(turret1Upgrades);
        Queue<TurretUpgrade> turret3Upgrades = new Queue<TurretUpgrade>(turret1Upgrades);

        turretUpgrades = new Dictionary<int, Queue<TurretUpgrade>>
        {
            { 0, turret1Upgrades },
            { 1, turret2Upgrades },
            { 2, turret3Upgrades },
        };

        currentTurretLevels = new Dictionary<int, TurretUpgrade>
        {
            { 0, null },
            { 1, null },
            { 2, null },
        };
    }

    private int GetTurretsCount()
    {
        return turretUpgrades.Count;
    }

    public void UpgradeTurret(int turretIndex)
    {
        if (!turretUpgrades.ContainsKey(turretIndex)) return;

        Queue<TurretUpgrade> upgrades = turretUpgrades[turretIndex];
        if (upgrades.Count == 0)
        {
            Debug.Log("No more upgrades for " + turretIndex);
            return;
        }

        TurretUpgrade nextUpgrade = upgrades.Peek();
        team.RemoveGold(nextUpgrade.GetUpgradeCost());
        ApplyUpgrade(turretIndex);
    }

    private void ApplyUpgrade(int turretIndex)
    {
        TurretUpgrade nextUpgrade = turretUpgrades[turretIndex].Dequeue();

        // Upgrade current entity level
        currentTurretLevels[turretIndex] = nextUpgrade;
    }

    public TurretUpgrade GetTurretUpgrade(int turretIndex)
    {
        return currentTurretLevels[turretIndex];
    }

    public bool CanUpgradeTurret(int turretIndex)
    {
        // We can upgrade turrets only if the 3 turrets are active, the team age is the same as the turret age
        return turretUpgrades[turretIndex].Count > 0 &&
               team.GetGold() >= turretUpgrades[turretIndex].Peek().GetUpgradeCost();
    }

    public bool CanUpgradeTurrets()
    {
        // Check if we can upgrade all turrets by checking if we can upgrade all turrets and if we have enough gold to buy all the turrets without using All() method
        foreach (var (i, turretUpgrade) in turretUpgrades)
        {
            if (turretUpgrade.Count == 0)
            {
                return false;
            }
        }

        // check if we can buy all turrets
        return (turretUpgrades[0].Peek().GetUpgradeCost() * GetTurretsCount()) <= team.GetGold();
    }
}