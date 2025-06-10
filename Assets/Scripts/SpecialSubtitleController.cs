using System.Collections;
using UnityEngine;

public class SpecialSubtitleController : MonoBehaviour
{
    [SerializeField] private ZookeeperSubtitle subtitleSystem;
    [SerializeField] private AudioClip specialClip;

    // Optional: assign a separate AudioSource if needed
    [SerializeField] private AudioSource specialAudioSource;

    private bool hasPlayedSpecial = false;

    public bool HasPlayedSpecial => hasPlayedSpecial;

    // Call this method when you want to trigger the special subtitle
    public void PlaySpecialSubtitle(string text, AudioClip clip = null)
    {
        if (hasPlayedSpecial) return;

        hasPlayedSpecial = true;
        StartCoroutine(PlaySpecialSubtitleRoutine(text, clip));
    }

    private IEnumerator PlaySpecialSubtitleRoutine(string text, AudioClip clip)
    {
        // If you want, mute zookeeper sound here:
        // For example: zookeeperRandomTalk.MuteAudio(true);

        // Use special audio clip or fallback
        AudioClip clipToUse = clip != null ? clip : specialClip;

        yield return subtitleSystem.SpeakCoroutine(text, clipToUse, -1f, true, true);

        // If you muted zookeeper sound, unmute here:
        // zookeeperRandomTalk.MuteAudio(false);
    }
}
