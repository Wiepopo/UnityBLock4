using UnityEngine;

public class PenguinRaycastDestroy : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float detectionDistance = 2f;
    [SerializeField] private GameObject penguinTextUI; // The text UI to be destroyed
    [SerializeField] private AudioClip destroySound; // Sound when text is destroyed
    [SerializeField] private AudioSource audioSource;

    private GameObject currentTargetPenguin;

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, detectionDistance))
        {
            // Check if the object is on the "Penguin" layer
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Penguin"))
            {
                currentTargetPenguin = hit.collider.gameObject;
                penguinTextUI.SetActive(true); // Show the text UI

                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Play the sound when "E" is pressed
                    if (destroySound != null && audioSource != null)
                    {
                        audioSource.PlayOneShot(destroySound);
                    }

                    penguinTextUI.SetActive(false); // Hide the text UI
                    currentTargetPenguin = null;    // Clear the target
                }

                return;
            }
        }

        penguinTextUI.SetActive(false); // Hide the text UI when not looking at a Penguin
        currentTargetPenguin = null;    // Clear the target if we are not pointing at a Penguin
    }
}
