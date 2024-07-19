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
    public void SpawnWave()
    {
        try
        {
            waves[spawnedWaves].SetActive(true);
            spawnedWaves++;
        }

        catch 
        {
            SceneManager.LoadScene("Game");
        }
        
    }


    private void Update()
    {
        
    }


    public void ChangeCameraToTopDown()
    {
        cameras[0].Priority = 10;
        cameras[1].Priority = 20;
    }


    Vector3 GetEnemySpawnLocation()
    {
        switch (enemySpawnPointIndex)
        {
            case 4:
                return new Vector3(14, 0.51f, 14);
            case 3:
                return new Vector3(14.5f, 0.51f, -13);
            case 2:
                return new Vector3(-13, 0.51f, 14);
            case 1:
                return new Vector3(-13.5f, 0.51f, -13);
            default:
                return new Vector3(-13.5f, 0.51f, -13);

        }
    }

    public void MoveWavesToSpawnPoint()
    {

        foreach (GameObject wave in waves)
        {
            wave.transform.position = GetEnemySpawnLocation();
        }
    }

}
