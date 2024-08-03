using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : BuildingBase
{
    private int[] aiHpPerLevel;
    public override void DefaultStatsSetup()
    {
        buildingType = BuildingType.Barracks;
        currentLevel = 0;

        hpPerLevel = new int[] { 50, 60, 70 };
        upgradePrices = new int[] { 4, 8, 16 };


        aiHpPerLevel = new int[] { 100, 110, 120 };

        profitPerLevel = new int[] { 4, 8, 12 };
        attackSpeedPerLevel = new int[] { 100, 100, 100 };
    }

    public override void CurrentStatsSetup()
    {
        healthComponent.maxHealth = hpPerLevel[currentLevel];
        healthComponent.currentHealth = hpPerLevel[currentLevel];
    }

    private void Awake()
    {
        allyRangedPrefab = Resources.Load<GameObject>("AllyInfantry");
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

    public override void AlliesSetup()
    {

        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "AllyInfantry(Clone)HealthBar")
            {
                Destroy(obj);
            }
        }
        allObjects = new GameObject[0];


        foreach (GameObject rangedAlly in rangedAllies)
        {
            Destroy(rangedAlly.gameObject);
        }
        rangedAllies = new List<GameObject>();

        for (int i = 0; i < profitPerLevel[currentLevel - 1]; i++)
        {

            GameObject newAllyRangedInstance;
            rangedAllies.Add(newAllyRangedInstance = Instantiate(allyRangedPrefab, spawnPoints[i].transform.position, Quaternion.identity));

            newAllyRangedInstance.transform.SetParent(transform.Find("AllyCounter").transform, true);


            newAllyRangedInstance.GetComponent<AllyInfantry>().health = aiHpPerLevel[currentLevel - 1];

            newAllyRangedInstance.GetComponent<Health>().maxHealth = newAllyRangedInstance.GetComponent<AllyInfantry>().health;
            newAllyRangedInstance.GetComponent<Health>().currentHealth = newAllyRangedInstance.GetComponent<AllyInfantry>().health;
            
    
        }


    }

    public override void RespawnAllyDuringBattle()
    {
        GameObject newAllyRangedInstance;
        rangedAllies.Add(newAllyRangedInstance = Instantiate(allyRangedPrefab, transform.Find("RespawnPointDuringBattle").position, Quaternion.identity));

        newAllyRangedInstance.transform.SetParent(transform.Find("AllyCounter").transform, true);


        newAllyRangedInstance.GetComponent<AllyInfantry>().health = aiHpPerLevel[currentLevel - 1];

        newAllyRangedInstance.GetComponent<Health>().maxHealth = newAllyRangedInstance.GetComponent<AllyInfantry>().health;
        newAllyRangedInstance.GetComponent<Health>().currentHealth = newAllyRangedInstance.GetComponent<AllyInfantry>().health;
    }




}
