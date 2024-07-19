using System.Collections;
using TMPro;
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
    }



    private void Update()
    {
        ClosestEnemyDetection();
        DrawRayToClosestObject();
        ShootArrowHandler();




    }

    [SerializeField] float currentArrowTimer;
    [SerializeField] float defaultArrowTimer;

    void ShootArrowHandler()
    {
        currentArrowTimer -= Time.deltaTime;

        if (enemyDistance <= radius && currentArrowTimer <= 0)
        {
            currentArrowTimer = defaultArrowTimer;
            SpawnArrow();
        }
    }





    private GameObject arrowPrefab;
    [SerializeField] float arrowSpeed;
    void SpawnArrow()
    {
        if (closestEnemy == null) return;


        GameObject newArrowInstance = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        newArrowInstance.GetComponent<Arrow>().target = closestEnemy.transform.position;
        newArrowInstance.GetComponent<Rigidbody>().AddForce((closestEnemy.transform.position - transform.position) * 100);
        newArrowInstance.GetComponent<Arrow>().speed = arrowSpeed;
    }












    // Get color
    float enemyDistance;
    private Color GetProximityColor()
    {
        if (closestEnemy == null) return Color.white;

        enemyDistance = Vector3.Distance(transform.position, closestEnemy.transform.position);
        if (enemyDistance <= radius) return Color.red;
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

            if (distance < neareastDistanceChanged)
            {
                closestEnemy = enemies[i];
                neareastDistanceChanged = distance;
            }

        }

        if (closestEnemy == null)
        {
            gameManager.SpawnWave();
        }
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
