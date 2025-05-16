using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PhotoSaveToGallery : MonoBehaviour
{
    public Camera photoCamera; // Assign the camera used for capturing
    public RawImage photoDisplayPrefab; // Prefab with a RawImage component to display photos
    public Transform galleryParent; // Parent transform to hold photo displays
    public GameObject PhotoGalleryPanel; //panel on which the different photos will be put
    public GameObject PhotoDisplayPrefab; // this is the ui prefab on which the screenshots will be projected
    public Transform GallaryContent; //content of the photo 

    private List<Texture2D> photoGallery = new List<Texture2D>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
    {
        PhotoGalleryPanel.SetActive(!PhotoGalleryPanel.activeSelf);
    }
    }
    public void CapturePhoto()
    {
        StartCoroutine(CapturePhotoCoroutine());
    }

    private IEnumerator CapturePhotoCoroutine()
    {
        yield return new WaitForEndOfFrame();

        // Set up RenderTexture
        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
        photoCamera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        photoCamera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenShot.Apply();
        photoCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        // Add the screenshot to the gallery
        photoGallery.Add(screenShot);

        // Display the photo in the gallery UI
        RawImage newPhoto = Instantiate(photoDisplayPrefab, galleryParent);
        newPhoto.texture = screenShot;
    }
}
