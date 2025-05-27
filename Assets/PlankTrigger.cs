using UnityEngine;

public class TriggerActivateObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate; // Assign in Inspector
    [SerializeField] private bool triggerOnlyOnce = true;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered && triggerOnlyOnce) return;

        if (other.CompareTag("Player"))
        {
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
                hasTriggered = true;
            }
        }
    }
}
