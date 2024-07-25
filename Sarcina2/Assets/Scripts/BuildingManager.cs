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

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GetBuildings();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        HasCastleBeenBuiltCheck();
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

    






}
