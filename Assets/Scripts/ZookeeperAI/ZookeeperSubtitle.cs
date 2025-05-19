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

    public void Speak(string line, AudioClip voiceClip, float customDuration = -1f)
    {
        if (onCooldown) return;

        if (subtitleRoutine != null)
            StopCoroutine(subtitleRoutine);

        subtitleRoutine = StartCoroutine(ShowLine(line, voiceClip));
    }

    private IEnumerator ShowLine(string line, AudioClip clip)
    {
        onCooldown = true;

        // Show subtitle
        subtitleText.text = line;
        subtitleText.gameObject.SetActive(true);

        // Play voice
        if (clip != null && voiceSource != null)
        {
            voiceSource.PlayOneShot(clip);
        }

        yield return new WaitForSeconds(displayDuration);

        subtitleText.gameObject.SetActive(false);
        subtitleText.text = "";

        // Cooldown delay
        yield return new WaitForSeconds(2f); // cooldown duration
        onCooldown = false;
    }
}
