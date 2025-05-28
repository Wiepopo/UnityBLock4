using UnityEngine;

public class ZoneUpdate2 : MonoBehaviour
{
    [Tooltip("Set this to the zone number for this trigger")]
    public int zoneNumber = 2;

    [Tooltip("Reference to your PhotoSaveToGallery script")]
    public PhotoSaveToGallery photoSaveScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (photoSaveScript != null)
            {
                photoSaveScript.currentPhotoZone = zoneNumber;
                Debug.Log($"Entered zone {zoneNumber}, updated currentPhotoZone.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (photoSaveScript != null)
            {
                // Optionally reset zone to 0 or something when leaving zone
                photoSaveScript.currentPhotoZone = 0;
                Debug.Log($"Exited zone {zoneNumber}, reset currentPhotoZone.");
            }
        }
    }
}