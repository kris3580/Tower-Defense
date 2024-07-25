using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archery : BuildingBase
{
    public override void DefaultStatsSetup()
    {
        buildingType = BuildingType.Archery;
        currentLevel = 0;

        hpPerLevel = new int[] { 50, 60, 70 };
        upgradePrices = new int[] { 4, 8, 16 };

        profitPerLevel = new int[] { 4, 8, 12 };
        attackSpeedPerLevel = new int[] { 100, 125, 150 };
    }

    public override void CurrentStatsSetup()
    {
        healthComponent.maxHealth = hpPerLevel[currentLevel];
        healthComponent.currentHealth = hpPerLevel[currentLevel];
    }

    private void Awake()
    {
        DefaultStatsSetup();
    }
}
