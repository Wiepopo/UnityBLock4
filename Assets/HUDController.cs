using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class HUDController : MonoBehaviour
{
    public static HUDController instance;
    
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] TMP_Text interactionText;

    public void EnableInteractionText(string text) 
    {
        interactionText.text = text + "(F)";
        interactionText.gameObject.SetActive(true);
    }
    public void DisableInteractionText() 
    {
        interactionText.gameObject.SetActive(false);
    }
}