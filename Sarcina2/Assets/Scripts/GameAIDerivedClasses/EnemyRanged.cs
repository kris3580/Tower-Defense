using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : GameAI
{

    [SerializeField] EnemyRangedType enemyRangedType;
    public bool isInShootingRange;

    public override void DefaultStatsSetup()
    {
        switch (enemyRangedType)
        {
            case EnemyRangedType.Lightweight:
                speed = 5;
                damage = 20;
                hp = 100;
                break;
            case EnemyRangedType.Heavy:
                speed = 5;
                damage = 50;
                hp = 200;
                break;
        }
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


        targetsArray = GameObject.FindGameObjectsWithTag("BuildingModelHandle");
        targets.AddRange(targetsArray);
        targets.Add(player);


        for (int i = 0; i < targets.Count; i++)
        {
            distance = Vector3.Distance(transform.position, targets[i].transform.position);

            if (distance < neareastDistanceChanged && targets[i].activeSelf)
            {
                if (targets[i].name == "Player" && distance <= 3)
                {
                    closestEnemy = targets[i];
                    break;
                }
                else if (targets[i].name != "Player")
                {
                    closestEnemy = targets[i];
                    neareastDistanceChanged = distance;
                }



            }

        }

    }


    private void Follow()
    {
        if (isInShootingRange)
        {
            agent.isStopped = true;
            transform.LookAt(closestEnemy.transform.position);
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(closestEnemy.transform.position);
        }
        
    }
    private void ResetLists()
    {
        targets.Clear();
        targetsArray = new GameObject[0];
    }
}

    enum EnemyRangedType
    {
        Lightweight, Heavy
    }   





