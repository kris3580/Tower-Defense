using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : BuildingBase
{
    public override void DefaultStatsSetup()
    {
        buildingType = BuildingType.Barracks;
        currentLevel = 0;

        // de aflat cat hp trebuie ca atare
        hpPerLevel = new int[] { 100, 110, 120 };
        upgradePrices = new int[] { 4, 8, 16 };

        profitPerLevel = new int[] { 4, 8, 12 };
        attackSpeedPerLevel = new int[] { 100, 100, 100 };
    }

    private void Awake()
    {
        DefaultStatsSetup();
    }
}
