using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Collections;

public class OutroVideoPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip firstClip;
    public CanvasGroup fadePanel; // Assign in inspector
    public float fadeDuration = 0.5f;

    void Start()
    {
        StartCoroutine(PlayVideoThenQuit());
    }

    IEnumerator PlayVideoThenQuit()
    {
        // Start fully black
        fadePanel.alpha = 1f;

        // Setup and prepare video
        videoPlayer.clip = firstClip;
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
            yield return null;

        // Play video
        videoPlayer.Play();

        // Fade from black to visible
        yield return StartCoroutine(Fade(1f, 0f));

        // Wait for video to finish
        while (videoPlayer.isPlaying)
            yield return null;

        // Fade from visible to black
        yield return StartCoroutine(Fade(0f, 1f));

        // Quit application or stop play mode in editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    IEnumerator Fade(float from, float to)
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadePanel.alpha = Mathf.Lerp(from, to, timer / fadeDuration);
            yield return null;
        }
        fadePanel.alpha = to;
    }
}
