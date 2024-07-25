using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : BuildingBase
{
    public override void DefaultStatsSetup()
    {
        buildingType = BuildingType.Wall;
        currentLevel = 0;

        hpPerLevel = new int[] { 100, 110 };
        upgradePrices = new int[] { 4, 12 };
        
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
