using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AnimalAIWander : MonoBehaviour
{
    [Header("Wandering Settings")]
    [SerializeField] float wanderRadius = 10f;           // Radius within which the agent will wander
    [SerializeField] float wanderInterval;          // Time interval between wander movements
    [SerializeField] float maxWanderAngle;         // Maximum angle deviation from forward direction
    [SerializeField] float walkspeed;   //Speed of traversal
    [Header("Obstacle Detection Settings")]
    [SerializeField] float obstacleDetectionDistance = 2f;   // Distance to detect obstacles
    [SerializeField] LayerMask obstacleLayer;  // Layer mask for obstacles make sure obstacles are in the obstacle layer and the obstacle layer is set in the insp[SerializeField]

    private NavMeshAgent agent;
    private float timer;
    

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderInterval;
    }

    void Start()
    {
        
    }

    void Update()
    {
        agent.speed = walkspeed;
        timer += Time.deltaTime;

        if (timer >= wanderInterval)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, NavMesh.AllAreas);
            if (newPos != transform.position)
            {
                agent.SetDestination(newPos);
            }
            
            timer = 0;
        }
    }

    // Generates a random position within a cone in front of the agent and projects it onto the NavMesh
    private Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        float initialAngle = 30f; //the angle it may choose out of when starting to move
        float maxAngle = 180f; //the maximum angle in case it gets stuck it can move out of the situation
        float angleIncrement = 15f; //the amount of angle is added to try to get the animal unstuck to make it smoother
        int maxAttemptsPerAngle = 10; //amount of times it can try to get an angle that works and doesn't walk into an obstacle

        float currentAngle = initialAngle;

        while (currentAngle <= maxAngle)
        {
            for (int i = 0; i < maxAttemptsPerAngle; i++)
            {
                float randomAngle = Random.Range(-currentAngle, currentAngle);
                Quaternion rotation = Quaternion.Euler(0, randomAngle, 0);
                Vector3 direction = rotation * transform.forward;
                float randomDistance = dist; // Set distance equal to wanderRadius
                Vector3 finalPosition = origin + direction.normalized * randomDistance;

                // Check for obstacles in the path
                if (!Physics.Raycast(origin, direction, obstacleDetectionDistance, obstacleLayer))
                {
                    NavMeshHit navHit;
                    if (NavMesh.SamplePosition(finalPosition, out navHit, dist, layermask))
                    {
                        return navHit.position;
                    }
                }
            }

            // Increment the angle for the next set of attempts
            currentAngle += angleIncrement;
        }

        // If no valid point found, return current position to trigger pause
        return origin;
    }
}