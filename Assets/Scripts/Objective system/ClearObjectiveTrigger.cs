using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ClearObjectiveTrigger : MonoBehaviour
{
    public GameObject theText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            theText.GetComponent<Text>().text = "Done with zone 1";
        }
    }
}