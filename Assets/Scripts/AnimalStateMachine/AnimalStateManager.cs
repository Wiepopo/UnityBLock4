using UnityEngine;
using UnityEngine.AI;

public class AnimalStateManager : MonoBehaviour
{
    AnimalBaseState currentState;
    public AnimalEating animalEating = new AnimalEating();
    public AnimalFollowing animalFollowing = new AnimalFollowing();
    public AnimalHiding animalHiding = new AnimalHiding();
    public AnimalIdle animalIdle = new AnimalIdle();
    public AnimalRunning animalRunning = new AnimalRunning();
    public AnimalSleeping animalSleeping = new AnimalSleeping();
    public AnimalTalking animalTalking = new AnimalTalking();
    public AnimalWalking animalWalking = new AnimalWalking();
    public NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component not found on the GameObject.");
        }
    }


    void Start()
    {
        //Starting state for the first statemachine
        currentState = animalWalking;
        //"this" refers to the context of the animal(this monobehaviour script)
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(AnimalBaseState state)
    {
        Debug.Log("Switching to the " + state.GetType().Name);
        currentState = state;
        state.EnterState(this);
    }
}
