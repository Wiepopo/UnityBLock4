using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ScreenshotCardUI : MonoBehaviour
{
    public RawImage photoImage;
    public FullscreenPhotoViewer fullscreenViewer;

    private Texture2D currentTexture;

    public void SetPhoto(Texture2D texture)
    {
        currentTexture = texture;
        photoImage.texture = texture;
    }

   public void OnClick()
{
    Debug.Log("Card clicked");
    if (fullscreenViewer != null && currentTexture != null)
    {
        fullscreenViewer.ShowPhoto(currentTexture);
    }
    else
    {
        Debug.LogWarning("Missing fullscreenViewer or texture.");
    }
}

}
