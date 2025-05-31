using System.Collections;
using UnityEngine;
using TMPro;

public class ZookeeperSubtitle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI subtitleText;
    [SerializeField] private float displayDuration = 3f;
    [SerializeField] private AudioSource voiceSource;

    private Coroutine subtitleRoutine;
    private bool onCooldown = false;

    public bool IsOnCooldown()
    {
        return onCooldown;
    }

    // 🆕 Added `forceOverride` and optional custom duration
    public void Speak(string line, AudioClip voiceClip, float customDuration = -1f, bool forceOverride = false)
    {
        if (onCooldown && !forceOverride)
        {
            return;
        }

        if (subtitleRoutine != null)
            StopCoroutine(subtitleRoutine);

        subtitleRoutine = StartCoroutine(ShowLine(line, voiceClip, customDuration));
    }

    private IEnumerator ShowLine(string line, AudioClip clip, float duration = -1f)
    {
        onCooldown = true;


        subtitleText.text = line;
        subtitleText.gameObject.SetActive(true);

        if (clip != null && voiceSource != null)
        {
            voiceSource.PlayOneShot(clip);
        }

        float showTime = (duration > 0) ? duration : displayDuration;
        yield return new WaitForSeconds(showTime);

        subtitleText.gameObject.SetActive(false);
        subtitleText.text = "";

        yield return new WaitForSeconds(2f);
        onCooldown = false;
    }
}
