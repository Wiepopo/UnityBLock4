using UnityEngine;

public class UIWrapper : MonoBehaviour
{
    public GameObject UIwrapper;         // The GameObject holding the Canvas/UI
    public GameObject theText;           // Text inside the UI
    public GameObject RawImage;          // RawImage inside the UI
    public GameObject cameracanvas;      // Optional camera canvas
    public Transform character;          // The character the UI should follow

    public void InitializeUI()
    {
        // 1. Enable the wrapper
        UIwrapper.SetActive(true);

        // 2. Set position and scale relative to character
        if (character != null)
        {
            UIwrapper.transform.position = character.position + Vector3.up * 2f;
            UIwrapper.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        }
        else
        {
            Debug.LogWarning("Character not assigned to GameManager.");
        }

        // 3. Enable UI components
        cameracanvas.SetActive(true);
        RawImage.SetActive(true);
        theText.SetActive(true);
    }
}