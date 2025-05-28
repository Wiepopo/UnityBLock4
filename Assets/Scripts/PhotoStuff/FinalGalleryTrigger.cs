using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class FinalGalleryTrigger : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;

    [SerializeField] private GameObject finalGalleryPanel;
    [SerializeField] private Transform finalGalleryContent;
    [SerializeField] private GameObject finalGalleryPrefab;
    [SerializeField] private GameObject takePhotoScript;
    [SerializeField] private GameObject takePhotoCanvas;
    [SerializeField] private GameObject optionsMenu;

    [SerializeField] private TMP_Text warningText;
    [SerializeField] private TMP_Text evidenceCountText; // <-- Assign this in the inspector
    [SerializeField] private int maxEvidence = 8;

    void Update()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            bool isOpen = !finalGalleryPanel.activeSelf;
            if (isOpen) PhotoSaveToGallery.BlockPauseESCThisFrame = true;

            if (TryInteract())
            {
                OpenFinalGallery();
            }

            if (PhotoSaveToGallery.BlockPauseESCThisFrame)
                optionsMenu.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && finalGalleryPanel.activeSelf)
        {
            CloseFinalGallery();
        }
    }

    void LateUpdate()
    {
        PhotoSaveToGallery.BlockPauseESCThisFrame = false;
    }

    bool TryInteract()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
        {
            return hit.collider.CompareTag("FinalGalleryTrigger");
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
        UpdateEvidenceCounter();
        PhotoSaveToGallery.BlockPauseESCThisFrame = true;
    }

    public void CloseFinalGallery()
    {
        finalGalleryPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (takePhotoScript != null) takePhotoScript.SetActive(true);
        if (takePhotoCanvas != null) takePhotoCanvas.SetActive(true);

        PhotoSaveToGallery.BlockPauseESCThisFrame = true;
    }

    public void SubmitAndEndGame()
    {
        int evidenceCount = PhotoSaveToGallery.GetGallery().Count;
        if (evidenceCount < maxEvidence)
        {
            if (warningText != null)
            {
                warningText.text = "You need to collect more evidences to submit.";
                warningText.gameObject.SetActive(true);
                StartCoroutine(HideWarningAfterSeconds(2f));
            }
            return;
        }

        Debug.Log("Evidence submitted. Ending game...");
        finalGalleryPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (takePhotoScript != null) takePhotoScript.SetActive(true);
        if (takePhotoCanvas != null) takePhotoCanvas.SetActive(true);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private IEnumerator HideWarningAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (warningText != null)
        {
            warningText.gameObject.SetActive(false);
        }
    }

    void UpdateEvidenceCounter()
    {
        int evidenceCount = PhotoSaveToGallery.GetGallery().Count;
        if (evidenceCountText != null)
        {
            evidenceCountText.text = $"Evidence Collected ({evidenceCount}/{maxEvidence})";
        }
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
            }
        }
    }
}
