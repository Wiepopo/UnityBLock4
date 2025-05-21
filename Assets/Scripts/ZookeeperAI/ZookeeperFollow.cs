using UnityEngine;
using UnityEngine.AI;

public class ZookeeperFollow : MonoBehaviour
{
    public Transform player;
    public float followDistance = 5f;

    public float closeWanderRadius = 2f;
    public float farWanderRadius = 5f;
    public float playerStillThreshold = 2f; // seconds of no movement to allow far wander

    public float wanderInterval = 4f;

    private NavMeshAgent agent;
    private float wanderTimer;
    private Vector3 lastPlayerPosition;
    private float playerStillTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 5.5f; // Increase this to make the zookeeper walk faster
        wanderTimer = wanderInterval;
        lastPlayerPosition = player.position;
        playerStillTime = 0f;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        agent.speed = Mathf.Lerp(2f, 6f, distanceToPlayer / 10f); // Increase speed based on distance

    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Track player movement
        if (Vector3.Distance(player.position, lastPlayerPosition) > 0.05f)
        {
            playerStillTime = 0f;
            lastPlayerPosition = player.position;
        }
        else
        {
            playerStillTime += Time.deltaTime;
        }

        // If too far, follow
        if (distanceToPlayer > followDistance)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            wanderTimer -= Time.deltaTime;

            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance + 0.1f)
            {
                if (wanderTimer <= 0f)
                {
                    float radius = (playerStillTime > playerStillThreshold) ? farWanderRadius : closeWanderRadius;
                    WanderNearPlayer(radius);
                    wanderTimer = Random.Range(wanderInterval - 1f, wanderInterval + 1.5f); // adds variety
                }
            }
        }
    }

    void WanderNearPlayer(float radius)
    {
        Vector3 randomOffset = Random.insideUnitSphere * radius;
        randomOffset.y = 0f;
        Vector3 target = player.position + randomOffset;

        if (NavMesh.SamplePosition(target, out NavMeshHit hit, radius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}
