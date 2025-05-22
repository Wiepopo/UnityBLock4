using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FullscreenPhotoViewer : MonoBehaviour
{
    public GameObject panel;
    public RawImage fullscreenPhoto;
    public TextMeshProUGUI captionText;

    public void ShowPhoto(Texture2D texture, string caption = "")
{
    Debug.Log("ShowPhoto called. Checking refs...");

    if (fullscreenPhoto == null)
        Debug.LogError("fullscreenPhoto is NULL!");
    if (captionText == null)
        Debug.LogWarning("captionText is NULL! Skipping caption assignment.");
    if (panel == null)
        Debug.LogError("panel is NULL!");

    fullscreenPhoto.texture = texture;

    // ✅ Only set text if captionText exists
    if (captionText != null)
        captionText.text = caption;

    panel.SetActive(true);
}


    public void ClosePhoto()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        if (panel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePhoto();
        }
    }
}
