using UnityEngine;

public class EvidenceIconSpawnerPoster : MonoBehaviour
{
    [Header("Assign the ! prefab")]
    [SerializeField] private GameObject exclamationPrefab;

    [Header("Offset of the icon from this object")]
    [SerializeField] private Vector3 iconLocalOffset = new Vector3(0.5f, 0f, 0f);

    private GameObject spawnedIcon;

    void Start()
    {
        if (exclamationPrefab != null)
        {
            spawnedIcon = Instantiate(exclamationPrefab, transform);
            spawnedIcon.transform.localPosition = iconLocalOffset;
        }
    }

    public void HideIcon()
    {
        if (spawnedIcon != null)
            spawnedIcon.SetActive(false);
    }
}
