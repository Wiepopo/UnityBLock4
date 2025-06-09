using UnityEngine;

public class FinalTrigger : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float detectionDistance = 2f;
    [SerializeField] private GameObject SubmitTextUI;

    void Update()
    {
        bool showText = false;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, detectionDistance))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject != null && hitObject.CompareTag("FinalGalleryTrigger") && hitObject.activeInHierarchy)
            {
                showText = true;
            }
        }

        // Only change UI state if necessary (optimization)
        if (SubmitTextUI.activeSelf != showText)
        {
            SubmitTextUI.SetActive(showText);
        }
    }
}
