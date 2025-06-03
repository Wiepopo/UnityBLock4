using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip zoneMusic;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        MusicManager manager = Object.FindFirstObjectByType<MusicManager>();

        if (manager != null)
        {
            manager.PlaySong(zoneMusic);
        }
    }
}
