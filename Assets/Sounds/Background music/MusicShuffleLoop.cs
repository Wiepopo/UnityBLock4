using System.Collections;
using UnityEngine;

public class MusicShuffleLoop : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip[] songs;
    [SerializeField] private float songDuration = 300f;         // Full play time per song (e.g., 5 minutes)
    [SerializeField] private float fadeDuration = 3f;           // Duration of fade in/out in seconds
    [SerializeField] private float targetVolume = 1f;           // Max volume

    private int lastPlayedIndex = -1;

    void Start()
    {
        if (musicSource == null || songs.Length < 2)
        {
            return;
        }

        musicSource.volume = 0f;
        StartCoroutine(PlayShuffledMusicLoop());
    }

    private IEnumerator PlayShuffledMusicLoop()
    {
        while (true)
        {
            int nextIndex = GetNextSongIndex();
            musicSource.clip = songs[nextIndex];
            musicSource.Play();

            // fade in
            yield return StartCoroutine(FadeVolume(0f, targetVolume, fadeDuration));

            // wait for (total duration - fade out time)
            yield return new WaitForSeconds(songDuration - fadeDuration);

            // fade out
            yield return StartCoroutine(FadeVolume(targetVolume, 0f, fadeDuration));

            musicSource.Stop();
        }
    }

    private IEnumerator FadeVolume(float from, float to, float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            musicSource.volume = Mathf.Lerp(from, to, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }
        musicSource.volume = to;
    }

    private int GetNextSongIndex()
    {
        int newIndex;
        do
        {
            newIndex = Random.Range(0, songs.Length);
        } while (newIndex == lastPlayedIndex);

        lastPlayedIndex = newIndex;
        return newIndex;
    }
}
