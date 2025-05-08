using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
    Outline outline;
    public string message;

    public UnityEvent onInteraction;
    
    void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
    }

    public void Interact()
    {
        onInteraction.Invoke();
        Debug.Log("Interacted");
    }
    public void DisableOutline()
    {
        outline.enabled = false;
    }

    public void EnableOutline()
    {
        outline.enabled = true;
    }

    
    void Update()
    {
        
    }
}