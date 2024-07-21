using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : BuildingBase
{
    private void Awake()
    {
        hpPerLevel = new int[] { 50, 60 };
        upgradePrices = new int[] { 2, 2 };

        profitPerLevel = new int[] { 1, 2 };
    }


}
