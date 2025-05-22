using UnityEngine;
using UnityEngine.InputSystem;

public class AnimalAImanager : MonoBehaviour
{
    private AnimalAIWander animalAIWander;
    private AnimalAIIdle animalAIIdle;

    //Statements
    bool switchmode = false;
    
    void Awake()
    {
        animalAIWander = GetComponent<AnimalAIWander>();
        animalAIIdle = GetComponent<AnimalAIIdle>();
    }

    void Start()
    {
        animalAIWander.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            switchmode = !switchmode;
        }
       // Debug.Log(switchmode);
        
            animalAIIdle.enabled = !switchmode;
            animalAIWander.enabled = switchmode;
        
       
    }
}
