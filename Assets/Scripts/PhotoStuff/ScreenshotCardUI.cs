using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

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

    public List<Texture2D> photoGallery; // Assigned from PhotoSaveToGallery

    public void OnClick()
    {
        if (fullscreenViewer != null && currentTexture != null)
        {
            fullscreenViewer.ShowPhoto(currentTexture, "", photoGallery);
        }
    }


}
