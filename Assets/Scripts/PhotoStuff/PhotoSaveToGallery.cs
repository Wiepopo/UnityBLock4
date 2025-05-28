using UnityEngine;
using UnityEngine.UI;
using TMPro;
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

    [Header("Evidence UI")]
    [SerializeField] private TMP_Text evidenceCounterText;
    public int maxEvidence = 8;
    private int collectedEvidence = 0;

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

        UpdateEvidenceUI(); // initialize UI
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
            if (takePhotoScript != null) takePhotoScript.SetActive(!isOpen);
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
        if (photo == null || photographedObject == null || !photographedObject.CompareTag("Evidence"))
        {
            Debug.LogWarning("Invalid photo or object");
            return;
        }

        if (GameManager.Instance != null && !GameManager.Instance.CanSavePhotoInZone(currentPhotoZone))
        {
            Debug.Log($"Cannot save photo in Zone {currentPhotoZone}: limit reached.");
            return;
        }

        GameManager.Instance?.SavePhotoInZone(currentPhotoZone);

        photoGallery.Add(photo);
        collectedEvidence++;
        UpdateEvidenceUI();

        GameObject newPhotoGO = Instantiate(PhotoDisplayPrefab, GallaryContent);
        ScreenshotCardUI cardUI = newPhotoGO.GetComponent<ScreenshotCardUI>();

        if (cardUI != null)
        {
            cardUI.SetPhoto(photo);
            cardUI.fullscreenViewer = fullscreenViewer;
            cardUI.photoGallery = photoGallery;
        }

        Debug.Log($"Photo saved successfully in Zone {currentPhotoZone}.");
    }

    void UpdateEvidenceUI()
    {
        if (evidenceCounterText != null)
            evidenceCounterText.text = $"Evidence Collected ({collectedEvidence}/{maxEvidence})";
    }

    public static List<Texture2D> GetGallery()
    {
        return photoGallery;
    }
}
