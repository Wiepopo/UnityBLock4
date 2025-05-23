using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PhotoSaveToGallery : MonoBehaviour
{
    [Header("References")]
    public Camera photoCamera;
    public GameObject PhotoGalleryPanel;           // Full gallery panel
    public GameObject PhotoDisplayPrefab;          // Prefab with ScreenshotCardUI and RawImage
    public Transform GallaryContent;               // ScrollView's content container
    public FullscreenPhotoViewer fullscreenViewer; // Assign this in Inspector!

    private static List<Texture2D> photoGallery = new List<Texture2D>();

    void Start()
    {
        PhotoGalleryPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            bool isOpen = !PhotoGalleryPanel.activeSelf;
            PhotoGalleryPanel.SetActive(isOpen);

            // Toggle mouse cursor visibility
            Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isOpen;
            if (!isOpen)
                Time.timeScale = 1f;
        }
    }

    public void SavePhoto(Texture2D photo)
    {
        if (photo == null) return;

        photoGallery.Add(photo);

        GameObject newPhotoGO = Instantiate(PhotoDisplayPrefab, GallaryContent);

        ScreenshotCardUI cardUI = newPhotoGO.GetComponent<ScreenshotCardUI>();

        if (cardUI != null)
        {
            cardUI.SetPhoto(photo);
            cardUI.fullscreenViewer = fullscreenViewer; // ✅ Assign viewer

            // Optional: assign the photo gallery list if needed
            cardUI.photoGallery = photoGallery;
        }
        else
        {
            Debug.LogWarning("ScreenshotCardUI script not found on prefab.");
        }
    }

    // Public getter for other scripts (like ScreenshotCardUI)
    public static List<Texture2D> GetGallery()
    {
        return photoGallery;
    }
}
