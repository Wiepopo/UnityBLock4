using UnityEngine;
using UnityEngine.InputSystem;

public class PhotoDetector : MonoBehaviour
{
    [SerializeField] private Camera photoCamera;
    [SerializeField] private float evidenceAmount;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame) // Replace with your photo key
        {
            TryDetectEvidence();
        }
    }

    public bool TryDetectEvidence()
    {
        Ray ray = photoCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, 10f))
        {
            if (hit.collider.CompareTag("Evidence"))
            {
                Debug.Log("Evidence found: " + hit.collider.name);
                
                return true;
            }
            else
            {
                Debug.Log("Hit something, but it's not evidence.");
            }
        }

        return false;
    }

}
