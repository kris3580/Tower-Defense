using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : BuildingBase
{
    public override void DefaultStatsSetup()
    {
        buildingType = BuildingType.Farm;
        currentLevel = 0;

        hpPerLevel = new int[] { 50 };
        upgradePrices = new int[] { 1 };

        profitPerLevel = new int[] { 1 };
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
