using UnityEngine;
using UnityEngine.AI;

public class ZookeeperFollow : MonoBehaviour
{
    public Transform player;
    public float followDistance = 5f;

    public float closeWanderRadius = 2f;
    public float farWanderRadius = 5f;
    public float playerStillThreshold = 2f;

    public float wanderInterval = 4f;

    private NavMeshAgent agent;
    private float wanderTimer;
    private Vector3 lastPlayerPosition;
    private float playerStillTime;

    private bool followEnabled = false;

    private float destinationUpdateThreshold = 1.5f; // Don't re-path unless player moved

    // Optional: Reference to Animator (assign in Inspector if used)
    public Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        wanderTimer = wanderInterval;
        lastPlayerPosition = player.position;
        playerStillTime = 0f;

        // Optional smoother settings
        agent.acceleration = 10f;
        agent.angularSpeed = 180f;
        agent.stoppingDistance = 1.2f;
        agent.autoBraking = true;
    }

    void Update()
    {
        if (!followEnabled) return;

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

        // Smooth follow logic
        if (distanceToPlayer > followDistance)
        {
            float distanceToCurrentTarget = Vector3.Distance(agent.destination, player.position);

            if (distanceToCurrentTarget > destinationUpdateThreshold)
            {
                agent.SetDestination(player.position);
            }
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
                    wanderTimer = Random.Range(wanderInterval - 1f, wanderInterval + 1.5f);
                }
            }
        }

        // Optional: Smooth manual rotation if agent.updateRotation is false
        if (agent.hasPath && agent.desiredVelocity.sqrMagnitude > 0.1f)
        {
            Vector3 direction = agent.steeringTarget - transform.position;
            direction.y = 0f;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3f);
            }
        }

        // Optional: Hook to animation
        if (animator != null)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
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

    public void EnableFollow()
    {
        Debug.Log("Zookeeper follow ENABLED");
        followEnabled = true;
    }
}
