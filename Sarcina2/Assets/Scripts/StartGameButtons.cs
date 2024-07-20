using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButtons : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject joystick;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    private void OnMouseDown()
    {
        switch (gameObject.name)
        {
            case "StartGameChoices_Choice (1)":
                player.transform.position = new Vector3(14, 1, 14);
                gameManager.enemySpawnPointIndex = 1;
                break;
            case "StartGameChoices_Choice (2)":
                player.transform.position = new Vector3(14.5f, 1, -13);
                gameManager.enemySpawnPointIndex = 2;
                break;
            case "StartGameChoices_Choice (3)":
                player.transform.position = new Vector3(-13, 1, 14);
                gameManager.enemySpawnPointIndex = 3;
                break;
            case "StartGameChoices_Choice (4)":
                player.transform.position = new Vector3(-13.5f, 1, -13);
                gameManager.enemySpawnPointIndex = 4;
                break;
            case "StartGameChoices_Choice (5)":
                player.transform.position = new Vector3(0.5f, 1, 0);
                gameManager.enemySpawnPointIndex = Random.Range(1,5);
                break;
        }

        joystick.SetActive(true);
        player.SetActive(true);
        gameManager.ChangeCameraToTopDown();
        gameManager.MoveWavesToSpawnPoint();
        gameManager.SpawnWave();
        gameManager.wasSpawnPlacementSelected = true;

        Destroy(GameObject.Find("StartGameChoices").gameObject); ;

    }
}
