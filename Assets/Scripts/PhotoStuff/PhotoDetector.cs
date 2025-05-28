using UnityEngine;
using System.Collections;

public class PhotoDetector : MonoBehaviour
{
    [SerializeField] private Camera photoCamera;
    [SerializeField] private GameObject cameraManager;
    [SerializeField] private PhotoSaveToGallery photoSaveToGallery;

    public bool TryDetectEvidence(Texture2D photoTexture)
    {
        Ray ray = photoCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, 10f) && cameraManager.activeSelf)
        {
            if (hit.collider.CompareTag("Evidence"))
            {
                Debug.Log("Evidence found: " + hit.collider.name);

                StartCoroutine(ChangeTagAfterDelay(hit.collider.gameObject, 0.1f));
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
