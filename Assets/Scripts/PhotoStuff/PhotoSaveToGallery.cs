using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PhotoSaveToGallery : MonoBehaviour
{
    public Camera photoCamera;
    public GameObject PhotoGalleryPanel; // Full panel
    public GameObject PhotoDisplayPrefab; // The prefab with a RawImage
    public Transform GallaryContent; // Content area of scroll view

    private List<Texture2D> photoGallery = new List<Texture2D>();

    void Start()
    {
        PhotoGalleryPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            PhotoGalleryPanel.SetActive(!PhotoGalleryPanel.activeSelf);
        }
    }

    public void SavePhoto(Texture2D photo)
    {
        if (photo == null) return;

        photoGallery.Add(photo);

        GameObject newPhotoGO = Instantiate(PhotoDisplayPrefab, GallaryContent);
        RawImage rawImage = newPhotoGO.GetComponent<RawImage>();
        if (rawImage != null)
        {
            rawImage.texture = photo;
        }
        else
        {
            Debug.LogWarning("PhotoDisplayPrefab is missing a RawImage component.");
        }
    }
}