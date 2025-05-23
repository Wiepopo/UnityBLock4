using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class FullscreenPhotoViewer : MonoBehaviour
{
    public GameObject panel;
    public RawImage fullscreenPhoto;
    public TextMeshProUGUI captionText;

    private List<Texture2D> allPhotos = new List<Texture2D>();
    private int currentIndex = 0;

    // ✅ Public property for PauseMenuManager to check if open
    public bool IsOpen => panel != null && panel.activeSelf;

    public void ShowPhoto(Texture2D texture, string caption = "", List<Texture2D> gallery = null)
    {
        fullscreenPhoto.texture = texture;
        if (captionText != null) captionText.text = caption;
        panel.SetActive(true);

        // ⏸ Pause game while fullscreen is open
        Time.timeScale = 0f;

        if (gallery != null)
        {
            allPhotos = gallery;
            currentIndex = allPhotos.IndexOf(texture);
        }
    }

    public void ClosePhoto()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
        // Inform PauseMenuManager to block ESC this frame
        PauseMenuManager pauseMenu = Object.FindFirstObjectByType<PauseMenuManager>();

        if (pauseMenu != null)
            pauseMenu.BlockESCForOneFrame();
    }

    void Update()
    {
        if (!panel.activeSelf) return;

        // Close with ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePhoto();
        }

        // Left/Right arrows to flip images
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ShowNext();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ShowPrevious();
        }
    }

    void ShowNext()
    {
        if (allPhotos == null || allPhotos.Count == 0) return;

        currentIndex = (currentIndex + 1) % allPhotos.Count;
        fullscreenPhoto.texture = allPhotos[currentIndex];
    }

    void ShowPrevious()
    {
        if (allPhotos == null || allPhotos.Count == 0) return;

        currentIndex = (currentIndex - 1 + allPhotos.Count) % allPhotos.Count;
        fullscreenPhoto.texture = allPhotos[currentIndex];
    }
}
