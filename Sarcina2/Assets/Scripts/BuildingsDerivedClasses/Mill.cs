using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mill : BuildingBase
{
    public override void DefaultStatsSetup()
    {
        buildingType = BuildingType.Mill;
        currentLevel = 0;

        hpPerLevel = new int[] { 50, 50, 50 };
        upgradePrices = new int[] { 3, 4, 6 };

        profitPerLevel = new int[] { 1, 2, 6 };
    }

    private void Awake()
    {
        DefaultStatsSetup();
    }

}
