using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AnimalAIIdle))]
[RequireComponent(typeof(AnimalAIRunning))]
[RequireComponent(typeof(AnimalAIWander))]
[RequireComponent(typeof(NavMeshAgent))]
public class AnimalAImanager : MonoBehaviour
{
    private AnimalAIWander animalAIWander;
    private AnimalAIIdle animalAIIdle;
    private AnimalAIRunning animalAIRunning;

    //Statements
    private enum AIState { Idle, Wander, Run }
    private AIState currentState;

    //Different timers for switching between states\
    [Header("State durations")]
    [SerializeField] float wanderTime = 12;
    [SerializeField] float runTime = 5;
    [SerializeField] float idleTime = 7;

    //Weight a certain state has to determine what state is the AI more likely to be in, in percentatges 0.1 = 10%
    [Header("State weights (0-1)")]
    [Range(0f, 1f)] public float idleWeight = 0.4f;
    [Range(0f, 1f)] public float wanderWeight = 0.4f;
    [Range(0f, 1f)] public float runWeight = 0.2f;

    //Handles animations of the animals
    private Animator aAnimater;
    private float switchToNextStateTimer;

    void Awake()
    {
        animalAIWander = GetComponent<AnimalAIWander>();
        animalAIIdle = GetComponent<AnimalAIIdle>();
        animalAIRunning = GetComponent<AnimalAIRunning>();
        aAnimater = GetComponent<Animator>();
    }

    void Start()
    {
        SwitchState(); // statr with a random state

    }

    void Update()
    {
        switchToNextStateTimer -= Time.deltaTime;
        //Debug.Log(switchToNextStateTimer);

        if (switchToNextStateTimer <= 0f)
        {
            SwitchState();
        }
    }

    void SwitchState()
    {
        currentState = GetWeightedRandomState();
        ApplyState(currentState);
    }

    void ApplyState(AIState state)
    {
        AllAISetToFalse();


        switch (state)
        {
            case AIState.Idle:
                animalAIIdle.enabled = true;
                switchToNextStateTimer = idleTime;
                aAnimater.SetTrigger("");
                break;

            case AIState.Wander:
                animalAIWander.enabled = true;
                switchToNextStateTimer = wanderTime;
                aAnimater.SetTrigger("TrWalk");
                break;

            case AIState.Run:
                animalAIRunning.enabled = true;
                switchToNextStateTimer = runTime;
                aAnimater.SetTrigger("TrRun");
                break;
        }
    }

    AIState GetWeightedRandomState()
    {
        float totalWeight = idleWeight + wanderWeight + runWeight;
        float rand = Random.Range(0f, totalWeight);

        if (rand < idleWeight)
            return AIState.Idle;
        else if (rand < idleWeight + wanderWeight)
            return AIState.Wander;
        else
            return AIState.Run;
    }

    void AllAISetToFalse()
    {
        animalAIIdle.enabled = false;
        animalAIWander.enabled = false;
        animalAIRunning.enabled = false;
    }



}
