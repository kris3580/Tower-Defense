using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyInfantry : GameAI
{

    public int health;


    private void Start()
    {
        DefaultStatsSetup();
    }

    private void Update()
    {
        ResetLists();
        ClosestEnemyDetection();
        Follow();
        DrawRayToClosestObject();

    }




    private GameObject[] targetsArray;
    [SerializeField] List<GameObject> targets;
    private GameObject closestEnemy;
    [SerializeField] private float distance;
    private float neareastDistance = 10000;
    private float neareastDistanceChanged;



    // Closest enemy detection
    private void DrawRayToClosestObject()
    {
        if (closestEnemy == null) return;

        Debug.DrawLine(transform.position, closestEnemy.transform.position, Color.blue);
    }
    private void ClosestEnemyDetection()
    {
        neareastDistanceChanged = neareastDistance;


        targetsArray = GameObject.FindGameObjectsWithTag("Enemy");
        targets.AddRange(targetsArray);

        for (int i = 0; i < targets.Count; i++)
        {
            distance = Vector3.Distance(transform.position, targets[i].transform.position);

            if (distance < neareastDistanceChanged && targets[i].activeSelf)
            {
                closestEnemy = targets[i];
                neareastDistanceChanged = distance;
            }

        }


    }


    private void Follow()
    {
        if (closestEnemy != null)
        {
            agent.isStopped = false;
            agent.SetDestination(closestEnemy.transform.position);

        }
        else
        {
            agent.isStopped = true;
        }
    }
    private void ResetLists()
    {
        targets.Clear();
        targetsArray = new GameObject[0];
    }
}

