using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archery : BuildingBase
{
    private int[] aiHpPerLevel;
    public override void DefaultStatsSetup()
    {
        buildingType = BuildingType.Archery;
        currentLevel = 0;

        hpPerLevel = new int[] { 30, 40, 70 };
        aiHpPerLevel = new int[] { 50, 60, 70 };

        upgradePrices = new int[] { 4, 8, 16 };

        profitPerLevel = new int[] { 4, 8, 12 };
        attackSpeedPerLevel = new int[] { 100, 125, 150 };
    }

    public override void CurrentStatsSetup()
    {
        healthComponent.maxHealth = hpPerLevel[currentLevel];
        healthComponent.currentHealth = hpPerLevel[currentLevel];
    }

    private void Awake()
    {
        allyRangedPrefab = Resources.Load<GameObject>("AllyRanged");
        SpawnPointsSetup();
        DefaultStatsSetup();
    }




    public GameObject[] spawnPoints = new GameObject[12];
    private void SpawnPointsSetup()
    {
        for (int i = 0; i < gameObject.transform.Find("BuildingModelHandle").transform.Find("FlagPole").transform.Find("AllySpawnPoints").childCount; i++)
        {
            spawnPoints[i] = gameObject.transform.Find("BuildingModelHandle").transform.Find("FlagPole").transform.Find("AllySpawnPoints").GetChild(i).gameObject;

        }
    }



    public List<GameObject> rangedAllies = new List<GameObject>();
    public GameObject allyRangedPrefab;

    public override void RangedAlliesSetup()
    {
        foreach (GameObject rangedAlly in rangedAllies)
        {
            Destroy(rangedAlly.gameObject);
        }
        rangedAllies = new List<GameObject>();

        for (int i = 0; i < profitPerLevel[currentLevel-1]; i++)
        {
            
            GameObject newAllyRangedInstance;
            rangedAllies.Add(newAllyRangedInstance = Instantiate(allyRangedPrefab, spawnPoints[i].transform.position, Quaternion.identity));
            newAllyRangedInstance.GetComponent<AllyRanged>().health = aiHpPerLevel[currentLevel - 1];
            newAllyRangedInstance.GetComponent<Health>().maxHealth = newAllyRangedInstance.GetComponent<AllyRanged>().health;
            newAllyRangedInstance.GetComponent<Health>().currentHealth = newAllyRangedInstance.GetComponent<AllyRanged>().health;
            newAllyRangedInstance.GetComponent<ArrowShooting>().arrowTimer = newAllyRangedInstance.GetComponent<ArrowShooting>().baseArrowTimerSpeed * 100 / attackSpeedPerLevel[currentLevel-1];
           
        }


    }
}
