using UnityEngine;

public class TooltipTrigger : MonoBehaviour
{
    public GameObject tooltipPrefab;
    public Transform objectToHover; 

    private GameObject spawnedTooltip;
    private bool hasTriggered = false;
    private bool hasInteracted = false;

    void Update()
    {
        if (spawnedTooltip != null && !hasInteracted && Input.GetMouseButton(0))
        {
            Destroy(spawnedTooltip);
            hasInteracted = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            spawnedTooltip = Instantiate(tooltipPrefab);
            TooltipUI ui = spawnedTooltip.GetComponent<TooltipUI>();
            if (ui != null) ui.target = objectToHover;

            hasTriggered = true; 
        }
    }
}
