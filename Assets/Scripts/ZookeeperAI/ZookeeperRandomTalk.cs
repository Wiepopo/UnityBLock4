using System.Collections;
using UnityEngine;

public class ZookeeperRandomTalk : MonoBehaviour
{
    [SerializeField] private ZookeeperSubtitle subtitleSystem;
    [SerializeField] private string[] randomLines;
    [SerializeField] private AudioClip[] randomClips;

    [SerializeField] private float minDelay = 10f;
    [SerializeField] private float maxDelay = 25f;

    private bool isTalking = false;

    void Start()
    {
        StartCoroutine(RandomTalkRoutine());
    }

    IEnumerator RandomTalkRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(waitTime);

            int index = Random.Range(0, randomLines.Length);
            subtitleSystem.Speak(randomLines[index], randomClips.Length > index ? randomClips[index] : null);
        }
    }
}
