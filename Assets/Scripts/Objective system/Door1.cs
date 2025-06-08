using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Door1 : MonoBehaviour
{
   
    public GameObject theText;
    public AudioSource objFX;
    [Header("Prefab to Spawn When Door Opens")]
    public GameObject replacementPrefab;

    void Update()
    {
        if (GameManager.Instance == null) return;

        
        bool zone1EvidenceCollected = GameManager.Instance.PhotosTakenZone2 >= GameManager.Instance.GetMaxPhotosForZone(1);

        if (zone1EvidenceCollected)
        {
              objFX.Play();
            theText.SetActive(true);
            theText.GetComponent<Text>().text = "<color=green>Take evidence</color>";
            
            // Optionally spawn a replacement object
            if (replacementPrefab != null)
            {
                Instantiate(replacementPrefab, transform.position, transform.rotation);
            }

            // Destroy this door
            Destroy(gameObject);
        }
    }
}

