using UnityEngine;
using UnityEngine.InputSystem;

public class FinalGalleryRayTrigger : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;

    [SerializeField] private GameObject finalGalleryPanel;
    [SerializeField] private Transform finalGalleryContent;
    [SerializeField] private GameObject finalGalleryPrefab;
    [SerializeField] private GameObject takePhotoScript;
    [SerializeField] private GameObject takePhotoCanvas;

    // To block pause menu ESC for one frame
    public static bool BlockPauseESCThisFrame = false;


    void Update()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            bool isOpen = !finalGalleryPanel.activeSelf;
            if (isOpen) BlockPauseESCThisFrame = true;
            if (TryInteract())
            {
                OpenFinalGallery();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && finalGalleryPanel.activeSelf)
        {
            finalGalleryPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            BlockPauseESCThisFrame = true;

            if (takePhotoScript != null) takePhotoScript.SetActive(true);
        }
        
    }

    bool TryInteract()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
        {
            if (hit.collider.CompareTag("FinalGalleryTrigger"))
                return true;
        }
        return false;
    }


    void OpenFinalGallery()
    {
        finalGalleryPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (takePhotoScript != null) takePhotoScript.SetActive(false);
        if (takePhotoCanvas != null) takePhotoCanvas.SetActive(false);

        PopulateFinalGallery();
        PhotoSaveToGallery.BlockPauseESCThisFrame = true;
    }
    public void CloseFinalGallery()
    {
        finalGalleryPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (takePhotoScript != null) takePhotoScript.SetActive(true);
        if (takePhotoCanvas != null) takePhotoCanvas.SetActive(true);

        FinalGalleryRayTrigger.BlockPauseESCThisFrame = true;
    }

    public void SubmitAndEndGame()
    {
        Debug.Log("Evidence submitted. Ending game...");

        // Close UI
        finalGalleryPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (takePhotoScript != null) takePhotoScript.SetActive(true);
        if (takePhotoCanvas != null) takePhotoCanvas.SetActive(true);

        // Optional: fade out or show message here

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // stop in editor
#else
    Application.Quit(); // quit in build
#endif
    }


    void PopulateFinalGallery()
    {
        foreach (Transform child in finalGalleryContent)
            Destroy(child.gameObject);

        foreach (Texture2D tex in PhotoSaveToGallery.GetGallery())
        {
            GameObject photoGO = Instantiate(finalGalleryPrefab, finalGalleryContent);
            ScreenshotCardUI card = photoGO.GetComponent<ScreenshotCardUI>();
            if (card != null)
            {
                card.SetPhoto(tex);
                // Optional: assign viewer
            }
        }
    }
}
