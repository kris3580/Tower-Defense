using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : BuildingBase
{
    private void Awake()
    {
        hpPerLevel = new int[] { 100, 110 };
        upgradePrices = new int[] { 4, 12 };
    }

}
