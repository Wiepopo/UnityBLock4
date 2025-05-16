using UnityEngine;

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
    void Start()
    {
        //Starting state for the first statemachine
        currentState = animalIdle;
        //"this" refers to the context of the animal(this monobehaviour script)
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(AnimalBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
