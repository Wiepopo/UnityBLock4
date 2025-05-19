using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZookeeperRandomTalk : MonoBehaviour
{
    [SerializeField] private ZookeeperSubtitle subtitleSystem;
    [SerializeField] private string[] randomLines;
    [SerializeField] private AudioClip[] randomClips;

    [SerializeField] private float minDelay = 10f;
    [SerializeField] private float maxDelay = 25f;

    private Queue<(string, AudioClip)> lineQueue;

    void Start()
    {
        InitializeQueue();
        StartCoroutine(RandomTalkRoutine());
    }

    void InitializeQueue()
    {
        List<(string, AudioClip)> combined = new List<(string, AudioClip)>();
        for (int i = 0; i < randomLines.Length; i++)
        {
            AudioClip clip = i < randomClips.Length ? randomClips[i] : null;
            combined.Add((randomLines[i], clip));
        }

        Shuffle(combined);
        lineQueue = new Queue<(string, AudioClip)>(combined);
    }

    IEnumerator RandomTalkRoutine()
    {
        while (lineQueue.Count > 0)
        {
            float waitTime = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(waitTime);

            if (!subtitleSystem) yield break;

            var (line, clip) = lineQueue.Dequeue();
            subtitleSystem.Speak(line, clip);
        }
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }
}
