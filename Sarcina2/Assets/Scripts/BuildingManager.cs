using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public List<GameObject> buildings = new List<GameObject>();
    public BuildingBase castle;
    public bool isCastleBuilt = false;
    private bool isCastleBuiltAlready = false;
    private GameManager gameManager;
    


    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GetBuildings();
    }




    private void Update()
    {
        HasCastleBeenBuiltCheck();

    }



    public void RepairBuildings()
    {
        foreach (GameObject building in buildings)
        {

            
            building.GetComponent<BuildingBase>().isRuined = false;
            building.transform.Find("BuildingModelHandle").GetComponent<Health>().transform.Find("BuildingModel").gameObject.SetActive(true);
            building.transform.Find("BuildingModelHandle").GetComponent<Health>().currentHealth = building.transform.Find("BuildingModelHandle").GetComponent<Health>().maxHealth;
            building.GetComponent<BuildingBase>().AlliesSetup();

        }
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
