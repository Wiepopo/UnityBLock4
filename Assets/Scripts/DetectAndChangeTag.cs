using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DetectAndChangeTag : MonoBehaviour
{
    [SerializeField] private Camera photoCamera;
    [SerializeField] private GameObject cameraManager;
    [SerializeField] private float evidenceAmount; // The amount of evidence needed
    [SerializeField] private PhotoSaveToGallery photoSaveToGallery; // Reference to your photo save script


    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame) // Replace with your photo key if different
        {
            TryDetectEvidence();
        }
    }

    public bool TryDetectEvidence()
    {
        Ray ray = photoCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, 10f))
        {
            if (cameraManager.activeSelf)
            {
                if (hit.collider.CompareTag("Evidence"))
                {
                    Debug.Log("Evidence found: " + hit.collider.name);

                    // Hide the exclamation icon
                    EvidenceIconSpawner icon = hit.collider.GetComponent<EvidenceIconSpawner>();
                    if (icon != null)
                        icon.HideIcon();

                    // Optionally disable tag
                    StartCoroutine(ChangeTagAfterDelay(hit.collider.gameObject, 0.1f));

                    evidenceAmount -= 1;
                    return true;
                }

                else
                {
                    Debug.Log("Hit something, but it's not evidence.");
                }
            }
        }

        return false;
    }

    private IEnumerator ChangeTagAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.tag = "NoLongerEvidence";
    }
}
