using UnityEngine;

public class DetectAndChangeTag : MonoBehaviour
{
    [SerializeField] private Camera photoCamera;
    [SerializeField] float evidenceAmount; //the amount of evidence needed

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) // Replace with your photo key
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
                hit.collider.tag = "NoLongerEvidence";
                evidenceAmount -= 1f;
                Debug.Log("Amount of evidence left to collect: " + evidenceAmount);
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
