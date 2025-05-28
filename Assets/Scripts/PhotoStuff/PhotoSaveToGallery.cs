using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PhotoSaveToGallery : MonoBehaviour
{
    [Header("References")]
    public GameObject PhotoGalleryPanel;
    public GameObject PhotoDisplayPrefab;
    public Transform GallaryContent;
    public FullscreenPhotoViewer fullscreenViewer;

    [SerializeField] private GameObject takePhotoScript;
    [SerializeField] private GameObject takePhotoCanvas;
    [SerializeField] private GameObject finalGalleryPanel;

    public bool cameraIsActive = false;
    private static List<Texture2D> photoGallery = new List<Texture2D>();

    public int currentPhotoZone = 1;

    public static bool BlockPauseESCThisFrame = false;

    void Start()
    {
        PhotoGalleryPanel.SetActive(false);
        photoGallery.Clear();

        foreach (Transform child in GallaryContent)
        {
            Destroy(child.gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            bool isOpen = !PhotoGalleryPanel.activeSelf;
            PhotoGalleryPanel.SetActive(isOpen);

            Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isOpen;

            if (isOpen) BlockPauseESCThisFrame = true;

            if (takePhotoScript != null)
                takePhotoScript.SetActive(!isOpen);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PhotoGalleryPanel.activeSelf)
            {
                CloseGallery();
            }
        }

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
            cameraIsActive = true;
        else if (Keyboard.current.digit1Key.wasPressedThisFrame || Keyboard.current.digit3Key.wasPressedThisFrame || Keyboard.current.digit4Key.wasPressedThisFrame)
            cameraIsActive = false;

        if (PhotoGalleryPanel.activeInHierarchy || finalGalleryPanel.activeInHierarchy)
        {
            takePhotoCanvas.SetActive(false);
        }
        else
        {
            takePhotoScript.SetActive(cameraIsActive);
            takePhotoCanvas.SetActive(cameraIsActive);
        }

        Debug.Log(cameraIsActive ? "Active" : "Inactive");
    }

    public void CloseGallery()
    {
        PhotoGalleryPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        BlockPauseESCThisFrame = true;

        if (!Keyboard.current.digit2Key.isPressed)
            cameraIsActive = false;

        takePhotoScript.SetActive(cameraIsActive);
    }

    public void SavePhoto(Texture2D photo, GameObject photographedObject)
    {
        if (photo == null)
        {
            Debug.LogWarning("No photo texture provided.");
            return;
        }

        if (photographedObject == null)
        {
            Debug.LogWarning("No object was photographed.");
            return;
        }

        if (!photographedObject.CompareTag("Evidence"))
        {
            Debug.Log("Photo not saved — object is not tagged as 'Evidence'.");
            return;
        }

        if (GameManager.Instance != null)
        {
            if (!GameManager.Instance.CanSavePhotoInZone(currentPhotoZone))
            {
                Debug.Log($"Cannot save photo in Zone {currentPhotoZone}: limit reached.");
                return;
            }

            GameManager.Instance.SavePhotoInZone(currentPhotoZone);
        }

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

        Debug.Log($"Photo saved successfully in Zone {currentPhotoZone}.");
    }

    public static List<Texture2D> GetGallery()
    {
        return photoGallery;
    }
}
