using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Playerinteract : MonoBehaviour
{
   public float playerReach = 3f;
   Interactor currentInteractable;

    // Update is called once per frame
    void Update()
    {
        CheckInteraction();
        if (Input.GetKeyDown(KeyCode.F) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    void CheckInteraction() 
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out hit, playerReach))
        {
            if (hit.collider.tag == "Interactable")
            {
                Interactor newInteractable = hit.collider.GetComponent<Interactor>();
                //If it is not the current interactable
                if (currentInteractable && newInteractable != currentInteractable)
                {
                    currentInteractable.DisableOutline();
                }
                if (newInteractable.enabled)
                {
                    SetNewCurrentInteractable(newInteractable);
                }
                else // If interaction is not enabled
                {
                    DisableCurrentInteractable();
                }
            }
            else // no interactables
            {
                DisableCurrentInteractable(); 
            }
        }
        else // Nothing reaches
        {
            DisableCurrentInteractable();
        }
    }
    void SetNewCurrentInteractable(Interactor NewInteractable)
    {
        currentInteractable = NewInteractable;
        currentInteractable.EnableOutline();
        HUDController.instance.EnableInteractionText(currentInteractable.message);
    }
    void DisableCurrentInteractable() 
    {
        HUDController.instance.DisableInteractionText();
        if (currentInteractable)
        {
            currentInteractable.DisableOutline();
            currentInteractable = null;

        }
    }
}
