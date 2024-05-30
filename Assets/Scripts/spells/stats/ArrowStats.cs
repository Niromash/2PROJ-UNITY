﻿public class ArrowStats : SpellStats
{
    public ArrowStats()
    {
        damage = 15f;
        spellCount = 30;
        wavesCount = 4;
        cooldown = 200f;
        deploymentCost = 100f;
    }

    public override string GetName()
    {
        return "Arrow";
    }
}