using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mill : BuildingBase
{
    private void Awake()
    {
        hpPerLevel = new int[] { 50, 50, 50 };
        upgradePrices = new int[] { 3, 4, 6 };

        profitPerLevel = new int[] { 1, 2, 6 };
    }

}
