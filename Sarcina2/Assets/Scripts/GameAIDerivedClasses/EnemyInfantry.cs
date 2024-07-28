using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyInfantry : GameAI
{
    [SerializeField] EnemyInfantryType enemyInfantryType;

    public override void DefaultStatsSetup() 
    {
        switch (enemyInfantryType)
        {
            case EnemyInfantryType.Lightweight:
                speed = 5;
                damage = 20;
                hp = 100;
                break;
            case EnemyInfantryType.Heavy:
                speed = 5;
                damage = 50;
                hp = 200;
                break;
            case EnemyInfantryType.Boss:
                speed = 5;
                damage = 100;
                hp = 300;
                break;
        }

        healthComponent.currentHealth = hp;
        healthComponent.maxHealth = hp;
    }


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
        agent.SetDestination(closestEnemy.transform.position);
    }
    private void ResetLists()
    {
        targets.Clear();
        targetsArray = new GameObject[0];
    }
}

enum EnemyInfantryType 
{ 
    Lightweight, Heavy, Boss
}
