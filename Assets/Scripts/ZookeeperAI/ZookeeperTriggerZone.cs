using UnityEngine;

public class ZookeeperTriggerZone : MonoBehaviour
{
    public ZookeeperRandomTalk zookeeperTalk; // Assign this in the inspector
    public int factSetIndex = 0; // 0 for first set, 1 for second, etc.
    public bool triggerOnlyOnce = true;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            zookeeperTalk.TriggerTalk(factSetIndex);

            if (triggerOnlyOnce)
            {
                hasTriggered = true;
                gameObject.SetActive(false); // Deactivate trigger zone
            }
        }
    }
}
