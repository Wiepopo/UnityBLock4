using System.Collections;
using UnityEngine;
using TMPro;

public class ZookeeperSubtitle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI subtitleText;
    [SerializeField] private float displayDuration = 3f;
    [SerializeField] private AudioSource voiceSource;

    [SerializeField] private AudioSource specialSpeaker;
    public float DisplayDuration => displayDuration;

    private Coroutine subtitleRoutine;
    private bool onCooldown = false;

    public bool IsOnCooldown()
    {
        return onCooldown;
    }

    public void Speak(string line, AudioClip voiceClip, float customDuration = -1f, bool forceOverride = false, bool useSpecialSpeaker = false)
    {
        if (onCooldown && !forceOverride)
            return;

        if (subtitleRoutine != null)
            StopCoroutine(subtitleRoutine);

        subtitleRoutine = StartCoroutine(ShowLine(line, voiceClip, customDuration, useSpecialSpeaker));
    }

    private IEnumerator ShowLine(string line, AudioClip clip, float duration = -1f, bool useSpecialSpeaker = false)
    {
        onCooldown = true;

        subtitleText.text = line;
        subtitleText.gameObject.SetActive(true);

        float showTime = duration > 0 ? duration : displayDuration;

        if (clip != null)
        {
            AudioSource speaker = useSpecialSpeaker && specialSpeaker != null ? specialSpeaker : voiceSource;
            speaker.PlayOneShot(clip);
            showTime = clip.length;
        }

        yield return new WaitForSeconds(showTime);

        subtitleText.gameObject.SetActive(false);
        subtitleText.text = "";

        yield return new WaitForSeconds(2f);
        onCooldown = false;
    }

    // NEW: Coroutine that speaks and waits for it to finish before continuing
  // Add this method inside ZookeeperSubtitle.cs
public IEnumerator SpeakCoroutine(string line, AudioClip voiceClip, float customDuration = -1f, bool forceOverride = false, bool useSpecialSpeaker = false)
{
    Speak(line, voiceClip, customDuration, forceOverride, useSpecialSpeaker);

    // Wait for the subtitle to finish
    float waitTime = customDuration > 0 ? customDuration : (voiceClip != null ? voiceClip.length : displayDuration);

    yield return new WaitForSeconds(waitTime + 0.1f); // small buffer

    // Optional cooldown wait (from your original code)
    yield return new WaitForSeconds(2f);
}

}
