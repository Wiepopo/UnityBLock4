using UnityEngine;

public class SnapToPoint : MonoBehaviour
{
    [SerializeField] private GameObject placedVersion;      // the plank thats gonna appear
    [SerializeField] private AudioClip snapSound;           // sound when snapped

    private bool hasSnapped = false;                        // checks if its already snapped

    private void OnTriggerEnter(Collider other)
    {
        if (hasSnapped) return;                             // it has been already snapped

        if (other.CompareTag("SnapZone"))
        {
            hasSnapped = true;                              

            if (placedVersion != null)
                placedVersion.SetActive(true);

            // plays sounds on snap
            if (snapSound != null)
                AudioSource.PlayClipAtPoint(snapSound, transform.position);

            Destroy(gameObject);                            // removes the picked plank
        }
    }
}
