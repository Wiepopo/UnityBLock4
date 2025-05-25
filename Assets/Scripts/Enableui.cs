using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;

public class Enableui : MonoBehaviour
{
    public GameObject theText;
    public GameObject RawImage;
    public GameObject cameracanvas;
    public GameObject UIWrapper;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameracanvas.gameObject.SetActive(true);
        RawImage.gameObject.SetActive(true);
        theText.gameObject.SetActive(true);
        UIWrapper.gameObject.SetActive(true);
    }

}
