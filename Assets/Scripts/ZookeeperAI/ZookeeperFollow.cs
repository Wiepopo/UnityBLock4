using UnityEngine;
using UnityEngine.AI;

public class ZookeeperFollow : MonoBehaviour
{
    public Transform player;
    public float followDistance = 3f;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > followDistance)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.ResetPath(); // Stop moving if close enough
        }
    }
}
