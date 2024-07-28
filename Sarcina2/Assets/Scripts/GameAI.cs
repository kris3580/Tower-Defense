using UnityEngine;
using UnityEngine.AI;

public class GameAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public float speed;
    public int hp;
    public int damage;
    public GameObject player;
    public virtual void DefaultStatsSetup() { }

    public Health healthComponent;
    public ArrowShooting arrowShooting;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

        player = GameObject.Find("Player");
        healthComponent = GetComponent<Health>();
        try { arrowShooting = GetComponent<ArrowShooting>(); } catch { }
    }


}
