using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject optionsPanel;               // Assign your menu panel
    public FullscreenPhotoViewer fullscreenViewer; // Assign in Inspector

    private bool isPaused = false;
    private bool escBlockedThisFrame = false;

    void Update()
    {
        // Prevent ESC from doing anything this frame if blocked
        if (escBlockedThisFrame)
        {
            escBlockedThisFrame = false;
            return;
        }

        // If fullscreen viewer is open, do nothing
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

    // Call this from FullscreenPhotoViewer when ESC is used to close fullscreen
    public void BlockESCForOneFrame()
    {
        escBlockedThisFrame = true;
    }
}
