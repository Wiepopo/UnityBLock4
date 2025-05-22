using UnityEditor.Rendering;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyOnCollisiontest : MonoBehaviour
{
    public GameObject theText;


    [SerializeField] GameObject emptyBowl1;
    [SerializeField] GameObject fullBowl1;
    [SerializeField] GameObject emptyBowl2;
    [SerializeField] GameObject fullBowl2;
    [SerializeField] GameObject emptyBowl3;
    [SerializeField] GameObject fullBowl3;
    private void OnTriggerEnter(Collider other)
    {
         if (other.TryGetComponent<Feedable>(out var feedable))
        {
            GameObject hitBowl = other.gameObject;

            if (hitBowl == emptyBowl1)
            {
                emptyBowl1.SetActive(false);
                fullBowl1.SetActive(true);
            }
            else if (hitBowl == emptyBowl2)
            {
                emptyBowl2.SetActive(false);
                fullBowl2.SetActive(true);
            }
            else if (hitBowl == emptyBowl3)
            {
                emptyBowl3.SetActive(false);
                fullBowl3.SetActive(true);
            }
            else
            {
                // Not a recognized bowl
                return;
            }

            Debug.Log("Fed the animal!");
            Destroy(gameObject); // Destroy the food bag

            // GameManager logic
            if (GameManager.Instance == null)
            {
                Debug.LogError("GameManager.Instance is NULL. Add GameManager to the scene.");
                return;
            }

            if (GameManager.Instance.Interactions < GameManager.Instance.MaxInteractions)
            {
                GameManager.Instance.Interactions++;
                Debug.Log("Interaction + 1: " + GameManager.Instance.Interactions);

                if (GameManager.Instance.Interactions == GameManager.Instance.MaxInteractions)
                {
                    Debug.Log("Objective Completed");
                    theText.GetComponent<Text>().text = " <color=green>Feed the animals</color>";
                }
            }
        }
    }

}