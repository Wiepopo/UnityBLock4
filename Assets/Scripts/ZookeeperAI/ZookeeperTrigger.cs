using UnityEditor.Rendering;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ZookeeperTrigger : MonoBehaviour

{
    public GameObject theText;
    public ZookeeperSubtitle subtitleSystem;
    [TextArea]
    public string line;
    public AudioClip voiceClip;
    public float subtitleDuration = -1f; // -1 = auto-match audio length

    private bool alreadyTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (alreadyTriggered) return;

        if (other.CompareTag("Player"))
        {
            subtitleSystem.Speak(line, voiceClip, subtitleDuration);
            alreadyTriggered = true;
            theText.GetComponent<Text>().text = "Feed the animals";
        }
    }
}
