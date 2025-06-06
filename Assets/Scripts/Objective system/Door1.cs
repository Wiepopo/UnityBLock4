using UnityEngine;

public class Door1 : MonoBehaviour
{
    [Header("Prefab to Spawn When Door Opens")]
    public GameObject replacementPrefab;

    void Update()
    {
        if (GameManager.Instance == null) return;

        
        bool zone1EvidenceCollected = GameManager.Instance.PhotosTakenZone1 >= GameManager.Instance.GetMaxPhotosForZone(1);

        if (zone1EvidenceCollected)
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

