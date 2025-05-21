using UnityEngine;
using UnityEngine.AI;

public class AnimalWalking : AnimalBaseState
{
    private float walkRadius = 10f;
    private float walkingTimer = 5f;
    private float timer;

    public override void EnterState(AnimalStateManager animal)
    {
        timer = walkingTimer;
    }

    public override void UpdateState(AnimalStateManager animal)
    {
    timer += Time.deltaTime;

    if (timer >= walkingTimer)
        {
            Vector3 newPos = RandomNavSphere(animal.transform.position, walkRadius, NavMesh.AllAreas);
            if (animal.agent.SetDestination(newPos))
            {
                Debug.Log("Destination set to: " + newPos);
            }
            else
            {
                Debug.LogWarning("Failed to set destination.");
            }
            timer = 0;
        }
    }

    public override void OnCollisionEnter(AnimalStateManager animal)
    {
        // Handle collisions if necessary
    }

    private Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
}