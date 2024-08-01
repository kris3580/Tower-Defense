using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] List<CinemachineVirtualCamera> cameras;
    public int enemySpawnPointIndex;
    [SerializeField] public List<GameObject> waves;
    private int spawnedWaves = 0;
    public GameObject fightButton;


    public void SpawnWave()
    {
        try
        {
            waves[spawnedWaves].SetActive(true);
            spawnedWaves++;
        }

        catch 
        {
            Store.ResetCoins();
            SceneManager.LoadScene("Game");
        }
        
    }

}

