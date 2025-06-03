using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Prefab to Spawn When Door Opens")]
    public GameObject replacementPrefab;

    void Update()
    {
        if (GameManager.Instance == null) return;

        bool fedAllAnimals = GameManager.Instance.Interactions == GameManager.Instance.MaxInteractions;
        bool zone1EvidenceCollected = GameManager.Instance.PhotosTakenZone1 >= GameManager.Instance.GetMaxPhotosForZone(1);

        if (fedAllAnimals && zone1EvidenceCollected)
        {
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

