using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Interaction Settings")]
    public int Interactions = 0;
    public int MaxInteractions = 3;

    [Header("Photo Save Limits")]
    public int MaxPhotosAllowed = 8;

    [Header("Photos Taken Per Zone")]
    public int PhotosTakenZone1 = 0;
    public int PhotosTakenZone2 = 0;
    public int PhotosTakenZone3 = 0;

    [Header("Max Photos Per Zone")]
    [SerializeField] private int MaxPhotosZone1 = 3;
    [SerializeField] private int MaxPhotosZone2 = 3;
    [SerializeField] private int MaxPhotosZone3 = 2;

    public int PhotosTaken => PhotosTakenZone1 + PhotosTakenZone2 + PhotosTakenZone3;

    public int GetMaxPhotosForZone(int zone)
    {
        return zone switch
        {
            1 => MaxPhotosZone1,
            2 => MaxPhotosZone2,
            3 => MaxPhotosZone3,
            _ => 0
        };
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    // ✅ Check if another photo can be saved in a given zone
    public bool CanSavePhotoInZone(int zone)
    {
        if (PhotosTaken >= MaxPhotosAllowed)
            return false;

        return zone switch
        {
            1 => PhotosTakenZone1 < MaxPhotosZone1,
            2 => PhotosTakenZone2 < MaxPhotosZone2,
            3 => PhotosTakenZone3 < MaxPhotosZone3,
            _ => false
        };
    }

    // ✅ Save photo in a zone and update counters
    public void SavePhotoInZone(int zone)
    {
        if (!CanSavePhotoInZone(zone))
        {
            Debug.LogWarning($"Cannot save photo in zone {zone}. Limit reached.");
            return;
        }

        switch (zone)
        {
            case 1:
                PhotosTakenZone1++;
                break;
            case 2:
                PhotosTakenZone2++;
                break;
            case 3:
                PhotosTakenZone3++;
                break;
        }

        Debug.Log($"Photo saved in Zone {zone}. Total: {PhotosTaken}/{MaxPhotosAllowed}.");
    }

    // ✅ Global photo save check
    public bool CanSaveMorePhotos()
    {
        return PhotosTaken < MaxPhotosAllowed;
    }

    // Optional: Reset photo counters for testing or replay
    public void ResetPhotoCounters()
    {
        PhotosTakenZone1 = 0;
        PhotosTakenZone2 = 0;
        PhotosTakenZone3 = 0;
        Debug.Log("Photo counters reset.");
    }
}




