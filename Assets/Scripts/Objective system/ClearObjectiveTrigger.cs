using UnityEngine;
using UnityEngine.UI;

public class ClearObjectiveTrigger : MonoBehaviour
{
    public GameObject theText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            theText.GetComponent<Text>().text = "";
        }
    }
}