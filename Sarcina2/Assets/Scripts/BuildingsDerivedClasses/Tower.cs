using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : BuildingBase
{
    private void Awake()
    {
        hpPerLevel = new int[] { 50, 60, 70 };
        upgradePrices = new int[] { 3, 5, 15 };

        damagePerLevel = new int[] { 50, 60, 70 };
        attackSpeedPerLevel = new int[] { 100, 125, 150 };
    }

}
