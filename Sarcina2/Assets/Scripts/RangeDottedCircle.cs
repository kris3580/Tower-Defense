using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDottedCircle : MonoBehaviour
{
    public GameObject player;
    [SerializeField] Vector3 offset;
    private GameManager gameManager;
    private void Start()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (gameManager.wasSpawnPlacementSelected)
        {

            transform.position = player.transform.position + offset;
        }

    }
}
