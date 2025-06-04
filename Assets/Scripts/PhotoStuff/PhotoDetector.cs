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
            GameObject target = hit.collider.gameObject;

            if (target.CompareTag("Evidence"))
            {
                Debug.Log("Evidence found: " + target.name);

                // Save first, change tag after
                photoSaveToGallery.SavePhoto(photoTexture, target);
                StartCoroutine(ChangeTagAfterDelay(target, 0.2f)); // Slight delay to ensure save finishes

                return true;
            }
            else
            {
                Debug.Log($"Hit '{target.name}', but it's not evidence. Tag: {target.tag}");
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

