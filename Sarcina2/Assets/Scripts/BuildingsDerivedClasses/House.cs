using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : BuildingBase
{
    public override void DefaultStatsSetup()
    {
        buildingType = BuildingType.House;
        currentLevel = 0;

        hpPerLevel = new int[] { 50, 60 };
        upgradePrices = new int[] { 2, 2 };

        profitPerLevel = new int[] { 1, 2 };
    }

    private void Awake()
    {
        DefaultStatsSetup();
    }


}
