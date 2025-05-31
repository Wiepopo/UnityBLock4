using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DestroyOnCollisiontest : MonoBehaviour
{
    public GameObject theText;

    [SerializeField] GameObject emptyBowl1;
    [SerializeField] GameObject fullBowl1;
    [SerializeField] GameObject emptyBowl2;
    [SerializeField] GameObject fullBowl2;
    [SerializeField] GameObject emptyBowl3;
    [SerializeField] GameObject fullBowl3;

    [SerializeField] private ZookeeperRandomTalk zookeeperTalk;
    [SerializeField] private ZookeeperSubtitle subtitleSystem;
    [SerializeField] private AudioClip weirdLineVoiceClip;

    private bool hasPlayedWeirdSubtitle = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Feedable>(out var feedable))
        {
            GameObject hitBowl = other.gameObject;

            if (hitBowl == emptyBowl1)
            {
                emptyBowl1.SetActive(false);
                fullBowl1.SetActive(true);
            }
            else if (hitBowl == emptyBowl2)
            {
                emptyBowl2.SetActive(false);
                fullBowl2.SetActive(true);
            }
            else if (hitBowl == emptyBowl3)
            {
                emptyBowl3.SetActive(false);
                fullBowl3.SetActive(true);
            }
            else
            {
                return;
            }

          
            Destroy(gameObject);

            if (GameManager.Instance == null)
            {
                
                return;
            }

            if (GameManager.Instance.Interactions < GameManager.Instance.MaxInteractions)
            {
                GameManager.Instance.Interactions++;
               

                if (GameManager.Instance.Interactions == GameManager.Instance.MaxInteractions)
                {
                    
                    theText.GetComponent<Text>().text = "<color=green>Feed the animals</color>";
                }
            }
        }

        // ✅ Trigger special subtitle only once
        if (!hasPlayedWeirdSubtitle &&
            fullBowl1.activeSelf && fullBowl2.activeSelf && fullBowl3.activeSelf)
        {
            hasPlayedWeirdSubtitle = true;

           

            if (zookeeperTalk != null)
                zookeeperTalk.PauseTalk();

            if (subtitleSystem != null)
                subtitleSystem.Speak("Hmm that’s weird, I should check that out", weirdLineVoiceClip, 4f, true); // 🆕 forced override

            StartCoroutine(ResumeZookeeperTalkAfterDelay(6f));
        }
    }

    private IEnumerator ResumeZookeeperTalkAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (zookeeperTalk != null)
            zookeeperTalk.ResumeTalk();
    }
}
