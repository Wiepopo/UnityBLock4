using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class AnimalAIIdle : MonoBehaviour
{
    [SerializeField] float rotationSpeed; // speed at wich the agent rotates this is for testing purposes
    private NavMeshAgent agent;

    [SerializeField] float stationaryThreshold; //The amount of time between no movement and the start of the idleAnimation
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //Checks if the animal is moving and compares that to the stationarythreshold if the velocity is lower than the stationary threshold
        //it means the code sees the animal as stationary and starts the idle animation
        if (agent.velocity.magnitude < stationaryThreshold)
        {
            agent.transform.Rotate(0f, rotationSpeed, 0f); //this makes the animal just rotate for testing purposes change for animation!!!!!!!!!!!!
        }
    }
}
