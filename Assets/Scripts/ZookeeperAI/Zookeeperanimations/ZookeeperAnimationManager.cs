using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class ZookeeperAnimationManager : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

    [SerializeField] private float speedThreshold = 0.1f;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);

        // OPTIONAL: debug
        // Debug.Log("Agent Speed: " + speed);
    }
}
