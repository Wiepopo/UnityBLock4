#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Rendering;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZookeeperTrigger : MonoBehaviour
{
    [Header("Subtitle Settings")]
    public ZookeeperSubtitle subtitleSystem;
    [TextArea]
    public string line;
    public AudioClip voiceClip;
    public float subtitleDuration = -1f; // -1 = auto-match audio length

    [Header("Objective UI")]
    [SerializeField] private string missionObjectiveText = "Inspect the zoo";
    public GameObject theText;

    private bool alreadyTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (alreadyTriggered) return;

        if (other.CompareTag("Player"))
        {
            alreadyTriggered = true;

            // Trigger subtitle
            subtitleSystem.Speak(line, voiceClip, subtitleDuration);

            // Play sound and show objective text
            StartCoroutine(ShowObjective());
        }
    }

    private IEnumerator ShowObjective()
    {
       
        if (theText != null)
        {
            theText.SetActive(true);
            theText.GetComponent<Text>().text = missionObjectiveText;
        }

        yield break;
    }
}
