using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> waves;
    private int spawnedWaves = 0;
    public GameObject fightButton;
    private DayNightTransition dayNightTransition;
    private BuildingManager buildingManager;

    private void Awake()
    {
        dayNightTransition = GameObject.Find("DayNightTransition").GetComponent<DayNightTransition>();
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();

    }


    private void Update()
    {
        try
        {
            if (waves[spawnedWaves - 1].transform.childCount == 0 && !DayNightTransition.isDay)
            {

                fightButton.GetComponent<Button>().interactable = true;
                StartCoroutine(dayNightTransition.StartTransition());
                buildingManager.DayNightSetup(true);
                buildingManager.RepairBuildings();
            }
        } catch { }
        
    }




    public void SpawnWave()
    {
        try
        {
            waves[spawnedWaves].SetActive(true);
            spawnedWaves++;
            fightButton.GetComponent<Button>().interactable = false;
            StartCoroutine(dayNightTransition.StartTransition());
            buildingManager.DayNightSetup(false);
        }

        catch 
        {
            Store.ResetCoins();
            SceneManager.LoadScene("Game");
        }
        
    }


    

}

