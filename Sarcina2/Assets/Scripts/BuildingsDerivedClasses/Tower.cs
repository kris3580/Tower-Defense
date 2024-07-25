using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : BuildingBase
{
    public override void DefaultStatsSetup()
    {
        buildingType = BuildingType.Tower;
        currentLevel = 0;

        hpPerLevel = new int[] { 50, 60, 70 };
        upgradePrices = new int[] { 3, 5, 15 };

        damagePerLevel = new int[] { 50, 60, 70 };
        attackSpeedPerLevel = new int[] { 100, 125, 150 };
    }

    public override void CurrentStatsSetup()
    {
        healthComponent.maxHealth = hpPerLevel[currentLevel];
        healthComponent.currentHealth = hpPerLevel[currentLevel];
        arrowShooting.damage = damagePerLevel[currentLevel];
        arrowShooting.arrowTimer = arrowShooting.baseArrowTimerSpeed * 100 / attackSpeedPerLevel[currentLevel];
    }

    private void Awake()
    {
        DefaultStatsSetup();
    }

}
