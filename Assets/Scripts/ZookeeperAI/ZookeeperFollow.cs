// --- ZookeeperFollow.cs ---
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

    public Animator animator;
    public ZookeeperSubtitle subtitleSystem;
    public AudioClip waitVoiceClip;

    private NavMeshAgent agent;
    private float wanderTimer;
    private Vector3 lastPlayerPosition;
    private float playerStillTime;
    private bool followEnabled = false;
    private bool isWaiting = false;

    private Vector3? waitTarget = null;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        wanderTimer = wanderInterval;
        lastPlayerPosition = player.position;
        playerStillTime = 0f;

        agent.stoppingDistance = 0.5f;
        agent.autoBraking = true;
    }

    void Update()
    {
        if (!followEnabled && !waitTarget.HasValue) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (Vector3.Distance(player.position, lastPlayerPosition) > 0.05f)
        {
            playerStillTime = 0f;
            lastPlayerPosition = player.position;
        }
        else
        {
            playerStillTime += Time.deltaTime;
        }

        if (followEnabled && distanceToPlayer > followDistance)
        {
            if (Vector3.Distance(agent.destination, player.position) > 1.0f)
            {
                agent.SetDestination(player.position);
            }
        }
        else if (followEnabled)
        {
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

        if (animator != null)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
        

        if (waitTarget.HasValue && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && agent.velocity.sqrMagnitude < 0.01f)
        {
            waitTarget = null;
            isWaiting = true;
            agent.ResetPath();

            if (subtitleSystem != null)
            {
                subtitleSystem.Speak("Yeah go ahead, I'm gonna wait you here.", waitVoiceClip);
            }

            if (animator != null)
            {
                animator.SetFloat("Speed", 0f);
            }

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

        agent.ResetPath();
    }

    public void EnableFollow()
    {
        followEnabled = true;
    }

    public void WaitHere()
    {
        isWaiting = true;
        followEnabled = false;
        agent.ResetPath();

        if (subtitleSystem != null)
        {
            subtitleSystem.Speak("Yeah go ahead, I'm gonna wait you here.", waitVoiceClip);
        }

        if (animator != null)
        {
            animator.SetFloat("Speed", 0f);
        }
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
        agent.Warp(newPosition);
        followEnabled = true;
        isWaiting = false;
    }
}


