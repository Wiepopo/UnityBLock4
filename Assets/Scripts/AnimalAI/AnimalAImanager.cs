using UnityEngine;
using UnityEngine.InputSystem;

public class AnimalAImanager : MonoBehaviour
{
    private AnimalAIWander animalAIWander;
    private AnimalAIIdle animalAIIdle;
    private AnimalAIRunning animalAIRunning;

    //Statements
    bool switchmodeactive = false;
    float stateCount = 0;

    //Different timers for switching between states\
    [SerializeField] float wanderTime;
    [SerializeField] float runTime;
    [SerializeField] float idleTime;
    private float switchToNextStateTimer;

    void Awake()
    {
        animalAIWander = GetComponent<AnimalAIWander>();
        animalAIIdle = GetComponent<AnimalAIIdle>();
        animalAIRunning = GetComponent<AnimalAIRunning>();
        
    }

    void Start()
    {
        
        
    }

    void Update()
    {
        switchToNextStateTimer -= Time.deltaTime;
        //Debug.Log(switchToNextStateTimer);

        if (switchToNextStateTimer <= 0f)
        {
            StateSwitcher();
            Debug.Log("should've switched states now");
        }    
    }

    void StateSwitcher()
    {
        if (stateCount == 0)
        {
            AllAISetToFalse();
            animalAIIdle.enabled = true;
            switchToNextStateTimer = idleTime;
        }
        else if (stateCount == 1)
        {
            AllAISetToFalse();
            animalAIWander.enabled = true;
            switchToNextStateTimer = wanderTime;
        }
        else if (stateCount == 2)
        {
            AllAISetToFalse();
            animalAIRunning.enabled = true;
            switchToNextStateTimer = runTime;
        }
        else if (stateCount == 3)
        {
            AllAISetToFalse();
            animalAIWander.enabled = true;
            switchToNextStateTimer = wanderTime;
            stateCount = -1f;
        }
        stateCount += 1;
        Debug.Log(stateCount);
    }
    void AllAISetToFalse()
    {
        animalAIIdle.enabled = false;
        animalAIWander.enabled = false;
        animalAIRunning.enabled = false;
    }


    
}
