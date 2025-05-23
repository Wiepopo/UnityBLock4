using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.InputSystem;
public class PhotoCapture : MonoBehaviour
{
    [Header("Photo Taker")]
    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private GameObject photoFrame;
    [SerializeField] private GameObject cameraUI;
    [SerializeField] private GameObject inventoryCanvas;

    [Header("Flash Effect")]
    [SerializeField] private GameObject cameraFlash;
    [SerializeField] private float flashTime;


    [Header("Photo Fader Effect")]
    [SerializeField] private Animator fadingAnimation;

    [Header("Audio")]
    [SerializeField] private AudioSource cameraAudio;

    [Header("Photosave")]
    [SerializeField] PhotoSaveToGallery photoSaveToGallery;

    [Header("Evidence")]
    [SerializeField] private PhotoDetector photoDetector;



    private Texture2D screenCapture;
    private bool viewingPhoto;

    private void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (!viewingPhoto)
            {
                StartCoroutine(CapturePhoto());
            }
            else
            {
                RemovePhoto();
            }
        }
    }

    IEnumerator CapturePhoto()
{
    cameraUI.SetActive(false);
        inventoryCanvas.SetActive(false);
    viewingPhoto = true;

    yield return new WaitForEndOfFrame();

    Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);
    screenCapture.ReadPixels(regionToRead, 0, 0, false);
    screenCapture.Apply();

    if (photoDetector.TryDetectEvidence())
    {
        Texture2D photoCopy = new Texture2D(screenCapture.width, screenCapture.height, screenCapture.format, false);
        photoCopy.SetPixels(screenCapture.GetPixels());
        photoCopy.Apply();

        photoSaveToGallery.SavePhoto(photoCopy);
    }

    ShowPhoto();
}



    void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplayArea.sprite = photoSprite;

        photoFrame.SetActive(true);

        //Do flash
        StartCoroutine(CameraFlashEffect());

        fadingAnimation.Play("PhotoFade");

    }

    IEnumerator CameraFlashEffect()
    {
        //play audio
        cameraAudio.Play();
        cameraFlash.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        cameraFlash.SetActive(false);
    }

    void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);
        //CameraUI true 
        cameraUI.SetActive(true);
        inventoryCanvas.SetActive(true);
    }
}
