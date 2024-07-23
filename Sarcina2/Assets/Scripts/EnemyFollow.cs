using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;

    [SerializeField] float speed;
    [SerializeField]

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    void Update()
    {

        if (player == null) player = GameObject.Find("BuildingModel");
        FollowPlayer();
    }

    void FollowPlayer()
    {

        if (player != null)
        {
            agent.speed = speed;
            agent.SetDestination(player.transform.position);
        }
            
    }


}
