using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroVideoPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName = "Zone1Playable";

    void Start()
    {
        StartCoroutine(PlayAndWait());
    }

    IEnumerator PlayAndWait()
    {
        videoPlayer.Prepare();

        // Wait for video to be prepared
        while (!videoPlayer.isPrepared)
            yield return null;

        videoPlayer.Play();

        // Wait for video to finish
        while (videoPlayer.isPlaying)
            yield return null;

        SceneManager.LoadScene(nextSceneName);
    }
}
