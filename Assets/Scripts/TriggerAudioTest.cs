using UnityEngine;

public class TriggerAudioTest : MonoBehaviour
{
    public AudioObject clipToPlay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("collided");
            Vocals.instance.Say(clipToPlay);
        }
    }
}