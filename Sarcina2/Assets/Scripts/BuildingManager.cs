using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    private List<GameObject> buildings = new List<GameObject>();
    public BuildingBase castle;
    public bool isCastleBuilt = false;
    private bool isCastleBuiltAlready = false;
    private GameManager gameManager;
    //public bool isRuined;


    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GetBuildings();
    }


    private void Update()
    {
        HasCastleBeenBuiltCheck();

        //if (castle)
        //{

        //}
    }

    private void HasCastleBeenBuiltCheck()
    {
        if (castle.isBuilt && !isCastleBuiltAlready)
        {
            isCastleBuilt = true;
            isCastleBuiltAlready = true;

            foreach (GameObject building in buildings)
            {
                building.SetActive(true);
            }

            gameManager.fightButton.GetComponent<Button>().interactable = true;

        }
    }

    private void GetBuildings()
    {
        GameObject[] buidlingsArray = GameObject.FindGameObjectsWithTag("BuildingHandle");

        foreach (GameObject building in buidlingsArray)
        {
            buildings.Add(building);
            if (building.name == "Castle") castle = building.GetComponent<Castle>();
            else building.SetActive(false);
        }
    }

    
    public void DayNightSetup(bool isSettingUpForDay)
    {
        foreach (GameObject building in buildings)
        {
            building.transform.Find("BuildingTypeInfo").gameObject.SetActive(isSettingUpForDay);
        }
    }


}
