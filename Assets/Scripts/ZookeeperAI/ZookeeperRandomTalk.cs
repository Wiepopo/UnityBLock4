using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZookeeperRandomTalk : MonoBehaviour
{
    [SerializeField] private ZookeeperSubtitle subtitleSystem;

    [System.Serializable]
    public class FactSet
    {
        public string[] lines;
        public AudioClip[] clips;
    }

    [SerializeField] private FactSet[] factSets;
    [SerializeField] private float minDelay = 10f;
    [SerializeField] private float maxDelay = 25f;

    private Queue<(string, AudioClip)> lineQueue;
    private Coroutine talkRoutine;

    private bool isPaused = false;

    public void TriggerTalk(int factSetIndex)
    {
        if (factSetIndex < 0 || factSetIndex >= factSets.Length) return;

        if (talkRoutine != null)
        {
            StopCoroutine(talkRoutine);
        }

        InitializeQueue(factSets[factSetIndex]);
        talkRoutine = StartCoroutine(RandomTalkRoutine());
    }

    void InitializeQueue(FactSet set)
    {
        List<(string, AudioClip)> combined = new List<(string, AudioClip)>();
        for (int i = 0; i < set.lines.Length; i++)
        {
            AudioClip clip = i < set.clips.Length ? set.clips[i] : null;
            combined.Add((set.lines[i], clip));
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

            while (isPaused)
            {
                yield return null;
            }

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

    public void PauseTalk()
    {
     
        isPaused = true;
    }

    public void ResumeTalk()
    {
        
        isPaused = false;
    }
}
