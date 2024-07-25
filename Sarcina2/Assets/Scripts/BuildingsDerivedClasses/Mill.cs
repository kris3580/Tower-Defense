using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mill : BuildingBase
{
    private int[] upgradeFarmsCount = new int[] { 3, 2, 3 };
    public override void DefaultStatsSetup()
    {
        buildingType = BuildingType.Mill;
        currentLevel = 0;

        hpPerLevel = new int[] { 50, 50, 50 };
        upgradePrices = new int[] { 3, 4, 6 };

        profitPerLevel = new int[] { 1, 2, 6 };

    }

    public override void CurrentStatsSetup()
    {
        healthComponent.maxHealth = hpPerLevel[currentLevel];
        healthComponent.currentHealth = hpPerLevel[currentLevel];
        UnlockFarmHandler();
    }

    private void Awake()
    {
        DefaultStatsSetup();
    }


    private void UnlockFarmHandler()
    {
        for (int i = 0; i < upgradeFarmsCount[currentLevel]; i++)
        {
        repeatUnitlFinished:
            int x = Random.Range(0, 8);
            if (!farmsList[x].activeSelf)
            {
                farmsList[x].SetActive(true);
            }
            else
            {
                goto repeatUnitlFinished;
            }

        }
    }
}

   