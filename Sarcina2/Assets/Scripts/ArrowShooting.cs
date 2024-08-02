using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ArrowShooting : MonoBehaviour
{

    // Closest enemy detection
    private GameObject[] enemies;
    private GameObject closestEnemy;
    [SerializeField] private float distance;
    private float neareastDistance = 10000;
    private float neareastDistanceChanged;
    public int damage;

    // Range check
    [SerializeField] float radius;

    // Draw circle stuff
    private int segments = 100;

    // Next wave stuff
    private GameManager gameManager;



    private void Awake()
    {
        arrowPrefab = Resources.Load<GameObject>("Arrow");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        arrowTimer = 1f;
        player = GameObject.Find("Player");
    }



    private void Update()
    {
        

        if (gameObject.name.Contains("Enemy"))
        {
            ClosestEnemyDetectionEnemyRanged();
            IsInShootingRangeCheck();
            if (closestEnemy != null) transform.LookAt(closestEnemy.transform.position);
        }
        else if (gameObject.name.Contains("Ally"))
        {
            ClosestEnemyDetection();
            if (closestEnemy != null) transform.LookAt(closestEnemy.transform.position);

        }
        else
        {
            ClosestEnemyDetection();
        }

        DrawRayToClosestObject();
        ShootArrowHandler();

        if (gameObject.name == "Player" && PlayerMovement.isShooting)
        {
            player.transform.LookAt(closestEnemy.transform.position); 
        }



    }


    void IsInShootingRangeCheck()
    {
        if (enemyDistance <= radius - 1)
        {
            GetComponent<EnemyRanged>().isInShootingRange = true;
        }
        else
        {
            GetComponent<EnemyRanged>().isInShootingRange = false;
        }
    }



    [HideInInspector] public float baseArrowTimerSpeed = 1f;
    public float currentArrowTimer;
    public float arrowTimer;
    
    void ShootArrowHandler()
    {
        if (gameObject.name.Contains("Enemy") && gameObject.GetComponent<Health>().hasDied)
        {
            return;
        }


        currentArrowTimer -= Time.deltaTime;

        if (enemyDistance <= radius && currentArrowTimer <= 0)
        {
            
            currentArrowTimer = arrowTimer;
            SpawnArrow();
        }
        else
        {
            if (gameObject.name == "Player")
            {
                player.transform.Find("PlayerCapsule").GetComponent<Animator>().SetBool("isShooting", false);
                PlayerMovement.isShooting = false;
            }
            else if (gameObject.name.Contains("EnemyRanged"))
            {
                transform.Find("EnemyHolder").transform.Find("Devil Bat").GetComponent<Animator>().SetBool("isShooting", false);
                //
            }
        }

    }





    private GameObject arrowPrefab;
    [SerializeField] public float arrowSpeed;
    void SpawnArrow()
    {


        if (closestEnemy == null)
        {
            
            return;
        }

        if (gameObject.name == "Player") 
        { 
            player.transform.Find("PlayerCapsule").GetComponent<Animator>().SetBool("isShooting", true);
            PlayerMovement.isShooting = true;
        }
        else if (gameObject.name.Contains("EnemyRanged"))
        {
            transform.Find("EnemyHolder").transform.Find("Devil Bat").GetComponent<Animator>().SetBool("isShooting", true);

        }



        GameObject newArrowInstance = Instantiate(arrowPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        newArrowInstance.GetComponent<Arrow>().target = closestEnemy.transform.position + new Vector3(0,0.3f,0);
        newArrowInstance.GetComponent<Rigidbody>().AddForce((closestEnemy.transform.position - transform.position) * 100);
        newArrowInstance.GetComponent<Arrow>().speed = arrowSpeed;
        newArrowInstance.GetComponent<Arrow>().damage = damage;

        if (gameObject.tag == "EnemyPosition") 
        { 
            newArrowInstance.GetComponent<Arrow>().isArrowShotByEnemy = true;
        }
        else
        {
            newArrowInstance.GetComponent<Arrow>().isArrowShotByEnemy = false;
        }

    }






    // Get color
    float enemyDistance;
    private Color GetProximityColor()
    {
        if (closestEnemy == null) return Color.white;

        enemyDistance = Vector3.Distance(transform.position, closestEnemy.transform.position);
        if (enemyDistance <= radius) {  return Color.red; }
        else return Color.yellow;
    }


    // Closest enemy detection
    private void DrawRayToClosestObject()
    {
        if (closestEnemy == null) return;

        Debug.DrawLine(transform.position, closestEnemy.transform.position, GetProximityColor());
    }
    private void ClosestEnemyDetection()
    {
        neareastDistanceChanged = neareastDistance;

        enemies = GameObject.FindGameObjectsWithTag("EnemyPosition");

        for (int i = 0; i < enemies.Length; i++)
        {
            distance = Vector3.Distance(transform.position, enemies[i].transform.position);

            if (distance < neareastDistanceChanged && !enemies[i].GetComponent<Health>().hasDied)
            {
                closestEnemy = enemies[i];
                neareastDistanceChanged = distance;
            }

        }

    }

    private GameObject[] targetsArray;
    [SerializeField] List<GameObject> targets;
    private GameObject player;
    private void ClosestEnemyDetectionEnemyRanged()
    {
        neareastDistanceChanged = neareastDistance;


        targetsArray = GameObject.FindGameObjectsWithTag("BuildingModelHandle");
        targets.AddRange(targetsArray);
        targetsArray = new GameObject[0];
        targetsArray = GameObject.FindGameObjectsWithTag("Ally");
        targets.AddRange(targetsArray);
        targets.Add(player);


        for (int i = 0; i < targets.Count; i++)
        {
            distance = Vector3.Distance(transform.position, targets[i].transform.position);

            if (distance < neareastDistanceChanged && targets[i].activeSelf )
            {
                if (targets[i].name == "Player" && distance <= 40 )
                {
                    closestEnemy = targets[i];
                    break;
                }
                else if (targets[i].name != "Player")
                {
                    if (targets[i].transform.parent.gameObject.GetComponent<BuildingBase>() != null)
                    {
                        if (targets[i].transform.parent.gameObject.GetComponent<BuildingBase>().isRuined)
                        {
                            goto restart;
                        }
                    }

                    closestEnemy = targets[i];
                    neareastDistanceChanged = distance;
                }
            }
            restart:;
        }
        targetsArray = new GameObject[0];
        targets = new List<GameObject>();


    }








    // Draw circle stuff
    void OnDrawGizmos()
    {
        DrawCircleGizmo(transform.position, radius, segments, GetProximityColor());
    }

    void DrawCircleGizmo(Vector3 center, float radius, int segments, Color color)
    {
        float angleStep = 360f / segments;
        Vector3 previousPoint = center + new Vector3(radius, 0, 0);
        Vector3 nextPoint;

        Gizmos.color = color;

        for (int i = 1; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            nextPoint = center + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
            Gizmos.DrawLine(previousPoint, nextPoint);
            previousPoint = nextPoint;
        }
    }

}
