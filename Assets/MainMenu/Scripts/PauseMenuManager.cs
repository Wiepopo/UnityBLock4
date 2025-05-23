using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject optionsPanel;
    public FullscreenPhotoViewer fullscreenViewer;
    private bool isPaused = false;
    private bool escBlockedThisFrame = false;

    public void BlockESCForOneFrame()
    {
        escBlockedThisFrame = true;
    }

    void Update()
    {
        if (escBlockedThisFrame)
        {
            escBlockedThisFrame = false;
            return;
        }
     if (PhotoSaveToGallery.BlockPauseESCThisFrame)
    {
        PhotoSaveToGallery.BlockPauseESCThisFrame = false;
        return;
    }
        if (fullscreenViewer != null && fullscreenViewer.IsOpen)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                PauseGame();
            else
                ResumeGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        optionsPanel.SetActive(true);
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        optionsPanel.SetActive(false);
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
