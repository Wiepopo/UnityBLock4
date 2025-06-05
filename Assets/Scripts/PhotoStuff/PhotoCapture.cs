using UnityEngine;
using UnityEngine.UI;
using System.Collections;
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
    [SerializeField] private float flashTime = 0.2f;

    [Header("Photo Fader Effect")]
    [SerializeField] private Animator fadingAnimation;

    [Header("Audio")]
    [SerializeField] private AudioSource cameraAudio;

    [Header("Gallery Handler")]
    [SerializeField] private PhotoSaveToGallery photoSaveToGallery;  // Assign in Inspector!

    private Texture2D screenCapture;
    private bool viewingPhoto = false;

    void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        photoFrame.SetActive(false);
    }

    void Update()
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

        Texture2D photoCopy = new Texture2D(screenCapture.width, screenCapture.height, screenCapture.format, false);
        photoCopy.SetPixels(screenCapture.GetPixels());
        photoCopy.Apply();

        // Raycast from screen center to detect object tagged "Evidence"
        GameObject photographedObj = null;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            if (hit.collider.CompareTag("Evidence"))
            {
                photographedObj = hit.collider.gameObject;
                Debug.Log($"Detected Evidence object: {photographedObj.name}");
            }
        }

        // Call SavePhoto on the gallery manager
        if (photoSaveToGallery != null)
        {
            photoSaveToGallery.SavePhoto(photoCopy, photographedObj);
        }
        else
        {
            Debug.LogWarning("PhotoSaveToGallery reference is missing in PhotoCapture.");
        }

        ShowPhoto();
    }

    void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0, 0, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100f);
        photoDisplayArea.sprite = photoSprite;
        photoFrame.SetActive(true);
        StartCoroutine(CameraFlashEffect());
        fadingAnimation.Play("PhotoFade");

        //automatically closes the photo
        StartCoroutine(AutoHidePhoto());
    }
    IEnumerator AutoHidePhoto()
{
    yield return new WaitForSeconds(2f); // Delay for 2 seconds
    if (viewingPhoto)
    {
        RemovePhoto();
    }
}


    IEnumerator CameraFlashEffect()
    {
        cameraAudio.Play();
        cameraFlash.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        cameraFlash.SetActive(false);
    }

    public void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);
        cameraUI.SetActive(true);
        inventoryCanvas.SetActive(true);
    }
}
