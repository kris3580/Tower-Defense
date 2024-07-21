using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archery : BuildingBase
{
    private void Awake()
    {
        // de aflat cat hp trebuie ca atare
        hpPerLevel = new int[] { 50, 60, 70 };
        upgradePrices = new int[] { 4, 8, 16};

        profitPerLevel = new int[] { 4, 8, 12 };
        attackSpeedPerLevel = new int[] { 100, 125, 150 };
    }
}
