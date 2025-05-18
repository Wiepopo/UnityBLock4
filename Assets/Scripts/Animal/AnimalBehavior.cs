using UnityEngine;
using UnityEngine.AI;

public class AnimalBehavior : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;

    [SerializeField] private float petDistance = 2f;
    [SerializeField] private float roamRadius = 5f;
    [SerializeField] private float roamInterval = 4f;

    private Transform player;
    private bool canBePetted = true;
    private float roamTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        roamTimer = roamInterval;

        Invoke(nameof(Roam), 1f); // Wait 1 sec and move

    }

    void Update()
    {
        // Animate movement
        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);

        // Optional: stop animation flicker
        if (speed < 0.1f)
            agent.velocity = Vector3.zero;
        // Check for petting
        if (canBePetted && Vector3.Distance(transform.position, player.position) < petDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Pet();
            }
        }

        // Roam logic
        if (canBePetted && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance + 0.1f)
        {
            roamTimer -= Time.deltaTime;
            if (roamTimer <= 0f)
            {
                Roam();
                roamTimer = roamInterval;
            }
        }

    }

    void Pet()
    {
        canBePetted = false;
        agent.isStopped = true;
        animator.SetTrigger("Sit");

        Invoke(nameof(ResumeWalk), 3f);
    }

    void ResumeWalk()
    {
        agent.isStopped = false;
        canBePetted = true;
        roamTimer = roamInterval; // restart timer after petting
    }

    void Roam()
    {
        const int maxAttempts = 10;
        float minDistance = 1.2f; // Distance threshold to avoid stutter

        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * roamRadius + transform.position;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, roamRadius, NavMesh.AllAreas))
            {
                float distance = Vector3.Distance(transform.position, hit.position);
                if (distance > minDistance)
                {
                    agent.SetDestination(hit.position);
                    return;
                }
            }
        }


    }

    public void TriggerPetting()
    {
        if (!canBePetted) return;

        canBePetted = false;
        agent.isStopped = true;
        animator.SetTrigger("Sit");
        Invoke(nameof(ResumeWalk), 3f);
    }
}
