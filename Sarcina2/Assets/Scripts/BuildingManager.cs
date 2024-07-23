using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] List<GameObject> buildings = new List<GameObject>();

    private void Awake()
    {
        GetBuildings();
    }







    private void GetBuildings()
    {
        GameObject[] buidlingsArray = GameObject.FindGameObjectsWithTag("BuildingHandle");

        foreach (GameObject building in buidlingsArray)
        {
            buildings.Add(building);

        }
    }








}
