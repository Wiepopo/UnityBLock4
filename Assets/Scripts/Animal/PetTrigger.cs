using UnityEngine;

public class PetTrigger : MonoBehaviour
{
    public AnimalBehavior animal;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            animal.TriggerPetting();
        }
    }
}
