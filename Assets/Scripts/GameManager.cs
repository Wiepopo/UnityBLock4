using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int Interactions = 0;
    public int MaxInteractions = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // optional if you want persistence across scenes
        }
        else
        {
            Destroy(gameObject); // prevent duplicates
        }
    }
}
