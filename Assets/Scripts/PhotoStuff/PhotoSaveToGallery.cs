using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Unity.VisualScripting.FullSerializer.Internal;

public class PhotoSaveToGallery : MonoBehaviour
{
    [Header("References")]
    public Camera photoCamera;
    public GameObject PhotoGalleryPanel;           // Full gallery panel
    public GameObject PhotoDisplayPrefab;          // Prefab with ScreenshotCardUI and RawImage
    public Transform GallaryContent;               // ScrollView's content container
    public FullscreenPhotoViewer fullscreenViewer;
    [SerializeField] private GameObject takePhotoScript;
    [SerializeField] private GameObject takePhotoCanvas;
    private static List<Texture2D> photoGallery = new List<Texture2D>();


    // To block pause menu ESC for one frame
    public static bool BlockPauseESCThisFrame = false;
    //For blocking movement when the gallery is opened
    bool galleryOpen = false;

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

            Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isOpen;

            // Block pause menu ESC for this frame
            if (isOpen) BlockPauseESCThisFrame = true;


            if (takePhotoScript != null)
                takePhotoScript.SetActive(!isOpen);
            if (takePhotoCanvas != null)
                takePhotoCanvas.SetActive(!isOpen);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PhotoGalleryPanel.activeSelf)
            {
                PhotoGalleryPanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                BlockPauseESCThisFrame = true; // Block ESC opening pause menu


                if (takePhotoScript != null)
                    takePhotoScript.SetActive(true);
                if (takePhotoCanvas != null)
                    takePhotoCanvas.SetActive(true);

            }
        }
    }
    public void CloseGallery()
    {
        PhotoGalleryPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PhotoSaveToGallery.BlockPauseESCThisFrame = true;
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
            cardUI.fullscreenViewer = fullscreenViewer;
            cardUI.photoGallery = photoGallery;
        }
        else
        {
            Debug.LogWarning("ScreenshotCardUI script not found on prefab.");
        }
    }

    public static List<Texture2D> GetGallery()
    {
        return photoGallery;
    }
}
