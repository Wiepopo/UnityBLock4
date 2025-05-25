using UnityEngine;
using UnityEngine.UI;

public class Textuiscript : MonoBehaviour
{
    public GameObject theText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theText.GetComponent<Text>().fontSize = 50;
        theText.GetComponent<Text>().color = Color.red;
    }

  
}
