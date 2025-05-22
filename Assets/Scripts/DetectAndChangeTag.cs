using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DetectAndChangeTag : MonoBehaviour
{
    [SerializeField] private Camera photoCamera;
    [SerializeField] float evidenceAmount; //the amount of evidence needed

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
                StartCoroutine(ChangeTagAfterDelay(hit.collider.gameObject, 0.1f));
                evidenceAmount -= 1;
                return true;
            }
            else
            {
                Debug.Log("Hit something, but it's not evidence.");
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
