using UnityEngine;
public class EnableUI : MonoBehaviour
{
    public GameObject cameracanvas; // Assign this in the Inspector

    void Start()
    {
        // Ensure canvas is visible and correctly scaled
        cameracanvas.SetActive(true);
        cameracanvas.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
    }
}

