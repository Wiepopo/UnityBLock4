using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SnapToPoint : MonoBehaviour
{
    [SerializeField] private GameObject placedVersion;      // the plank thats gonna appear
    [SerializeField] private AudioClip snapSound;           // sound when snapped

    private bool hasSnapped = false;                        // checks if its already snapped
    public GameObject theText;

    private void OnTriggerEnter(Collider other)
    {
        if (hasSnapped) return;                             // it has been already snapped

        if (other.CompareTag("SnapZone"))
        {
            hasSnapped = true;
            

            if (placedVersion != null)
                placedVersion.SetActive(true);
            // plays sounds on snap
            if (snapSound != null)
                AudioSource.PlayClipAtPoint(snapSound, transform.position);
            if (theText !=null)
                theText.GetComponent<Text>().text = " <color=green>Find a way across the water</color>";
           
            Destroy(gameObject);                            // removes the picked plank
        }
    }
}
