using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : BuildingBase
{

    private void Awake()
    {
        hpPerLevel = new int[] { 100, 110, 120 };
        upgradePrices = new int[] { 3, 7, 20 };

        profitPerLevel = new int[] { 1, 2, 3 };
        damagePerLevel = new int[] { 50, 60, 70 };
        attackSpeedPerLevel = new int[] {100, 100, 100 };

    }




}
