using UnityEngine;

public class EvidenceIconSpawner : MonoBehaviour
{
    [Header("Assign the ! prefab")]
    [SerializeField] private GameObject exclamationPrefab;

    private GameObject spawnedIcon;

    void Start()
    {
        if (exclamationPrefab != null)
        {
            spawnedIcon = Instantiate(exclamationPrefab, transform);
            spawnedIcon.transform.localPosition = new Vector3(0, 2, 0); // Adjust height
        }
    }

    public void HideIcon()
    {
        if (spawnedIcon != null)
            spawnedIcon.SetActive(false);
    }
}
