using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;

    [SerializeField] float speed;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    void Update()
    {

        if (player == null) player = GameObject.Find("Player");
        FollowPlayer();
    }

    void FollowPlayer()
    {
        agent.speed = speed;
        agent.SetDestination(player.transform.position);
    }


}
