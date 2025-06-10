using UnityEngine;
using UnityEngine.AI;

public class ZookeeperFollow : MonoBehaviour
{
    public Transform player;
    public float followDistance = 3f;
    public float stopDistance = 1.5f;
    public float updateInterval = 0.3f;

    public float minWanderDistance = 2.5f;
    public float maxWanderDistance = 4f;
    public float wanderInterval = 4f;

    public Animator animator;
    public ZookeeperSubtitle subtitleSystem;
    public AudioClip waitVoiceClip;

    private NavMeshAgent agent;
    private float updateTimer;
    private float wanderTimer;

    private bool followEnabled = false;
    private bool isWaiting = false;

    private Vector3? waitTarget = null;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stopDistance;
        agent.autoBraking = true;

        updateTimer = 0f;
        wanderTimer = wanderInterval;
    }

    void Update()
    {
        if (!followEnabled && !waitTarget.HasValue) return;

        updateTimer += Time.deltaTime;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (followEnabled)
        {
            if (distanceToPlayer > followDistance && updateTimer >= updateInterval)
            {
                // Stay a small distance behind the player
                Vector3 followTarget = player.position - player.forward * stopDistance;

                // Keep follow target on navmesh
                if (NavMesh.SamplePosition(followTarget, out NavMeshHit hit, 1f, NavMesh.AllAreas))
                {
                    agent.SetDestination(hit.position);
                }

                updateTimer = 0f;
            }
            else if (distanceToPlayer <= followDistance && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance + 0.1f)
            {
                // Wander near player when close enough
                wanderTimer -= Time.deltaTime;

                if (wanderTimer <= 0f)
                {
                    WanderNearPlayer();
                    wanderTimer = Random.Range(wanderInterval - 1f, wanderInterval + 1.5f);
                }
            }
        }

        if (waitTarget.HasValue && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && agent.velocity.sqrMagnitude < 0.01f)
        {
            waitTarget = null;
            isWaiting = true;
            agent.ResetPath();

            if (animator != null)
                animator.SetFloat("Speed", 0f);
        }

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

        animator.SetFloat("Speed", agent.velocity.magnitude);
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

        agent.ResetPath();
    }

    public void EnableFollow()
    {
        followEnabled = true;
        isWaiting = false;
        waitTarget = null;
    }

    public void WaitHere()
    {
        isWaiting = true;
        followEnabled = false;
        agent.ResetPath();

        if (subtitleSystem != null)
        {
            subtitleSystem.Speak("", waitVoiceClip);
        }

        if (animator != null)
        {
            animator.SetFloat("Speed", 0f);
        }
    }

    public void CancelWait()
    {
        waitTarget = null;
        isWaiting = false;
        agent.ResetPath();
    }

    public void GoToAndWait(Vector3 position)
    {
        waitTarget = position;
        followEnabled = false;
        isWaiting = false;
        agent.SetDestination(position);
    }

    public void TeleportTo(Vector3 newPosition)
    {
        CancelWait();
        agent.Warp(newPosition);
        followEnabled = true;
        isWaiting = false;
    }
}
