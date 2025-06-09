using UnityEngine;

public class DoorInteractionUI : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float detectionDistance = 2f;
    [SerializeField] private GameObject unlockDoorTextUI;

    void Update()
    {
        bool showText = false;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, detectionDistance))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject != null && hitObject.CompareTag("Door") && hitObject.activeInHierarchy)
            {
                showText = true;
            }
        }

        // Only change UI state if necessary (optimization)
        if (unlockDoorTextUI.activeSelf != showText)
        {
            unlockDoorTextUI.SetActive(showText);
        }
    }
}
