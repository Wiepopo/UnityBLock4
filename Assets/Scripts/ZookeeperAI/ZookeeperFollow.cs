using UnityEngine;
using UnityEngine.AI;

public class ZookeeperFollow : MonoBehaviour
{
    public Transform player;

    public float followDistance = 5f;
    public float minWanderDistance = 2.5f;
    public float maxWanderDistance = 4f;

    public float wanderInterval = 4f;
    public float playerStillThreshold = 2f;

    private NavMeshAgent agent;
    private float wanderTimer;
    private Vector3 lastPlayerPosition;
    private float playerStillTime;
    private bool followEnabled = false;

    public Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        wanderTimer = wanderInterval;
        lastPlayerPosition = player.position;
        playerStillTime = 0f;

        agent.stoppingDistance = minWanderDistance;
        agent.autoBraking = true;
    }

    void Update()
    {
        if (!followEnabled) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Track if the player is moving
        if (Vector3.Distance(player.position, lastPlayerPosition) > 0.05f)
        {
            playerStillTime = 0f;
            lastPlayerPosition = player.position;
        }
        else
        {
            playerStillTime += Time.deltaTime;
        }

        // If too far, follow directly
        if (distanceToPlayer > followDistance)
        {
            if (Vector3.Distance(agent.destination, player.position) > 1.0f)
            {
                agent.SetDestination(player.position);
            }
        }
        else
        {
            // Wander nearby when close to player
            wanderTimer -= Time.deltaTime;

            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance + 0.1f)
            {
                if (wanderTimer <= 0f)
                {
                    WanderNearPlayer();
                    wanderTimer = Random.Range(wanderInterval - 1f, wanderInterval + 1.5f);
                }
            }
        }

        // Optional smooth look
        if (agent.hasPath && agent.desiredVelocity.sqrMagnitude > 0.1f)
        {
            Vector3 dir = agent.steeringTarget - transform.position;
            dir.y = 0f;
            if (dir.sqrMagnitude > 0.01f)
            {
                Quaternion rot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 3f);
            }
        }

        // Optional animation
        if (animator != null)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
            Debug.Log("Zookeeper speed: " + agent.velocity.magnitude);

        }
    }

    void WanderNearPlayer()
    {
        const int maxAttempts = 10;

        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 offset = Random.insideUnitSphere * maxWanderDistance;
            offset.y = 0f;

            Vector3 target = player.position + offset;
            float dist = Vector3.Distance(target, player.position);

            if (dist >= minWanderDistance && dist <= maxWanderDistance &&
                NavMesh.SamplePosition(target, out NavMeshHit hit, maxWanderDistance, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
                return;
            }
        }

        // fallback
        agent.ResetPath();
    }

    public void EnableFollow()
    {
        followEnabled = true;
    }
}
