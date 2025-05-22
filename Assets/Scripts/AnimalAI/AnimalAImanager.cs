using UnityEngine;
using UnityEngine.InputSystem;

public class AnimalAImanager : MonoBehaviour
{
    private AnimalAIWander animalAIWander;
    private AnimalAIIdle animalAIIdle;
    private AnimalAIRunning animalAIRunning;

    //Statements
    bool switchmodeactive = false;

    //Different timers for switching between states\
    [SerializeField] float wanderTime;
    [SerializeField] float runTime;
    [SerializeField] float idleTime;
    [SerializeField] float switchToNextStateTimer;
    private float currentTime;

    void Awake()
    {
        animalAIWander = GetComponent<AnimalAIWander>();
        animalAIIdle = GetComponent<AnimalAIIdle>();
        animalAIRunning = GetComponent<AnimalAIRunning>();
        
    }

    void Start()
    {
        animalAIWander.enabled = false;
        animalAIRunning.enabled = false;
        currentTime = switchToNextStateTimer;
    }

    void Update()
    {
        if (currentTime > 0f)
        {
            currentTime -= Time.deltaTime;
        }

        if (currentTime <= 0f)
            {
                switchmodeactive = !switchmodeactive;
                currentTime = wanderTime;
            }
        
        animalAIIdle.enabled = !switchmodeactive;
        animalAIWander.enabled = switchmodeactive;
    }
}
