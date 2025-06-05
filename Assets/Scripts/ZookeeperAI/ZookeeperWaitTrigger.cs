// --- ZookeeperWaitTrigger.cs ---
using UnityEngine;

public class ZookeeperWaitTrigger : MonoBehaviour
{
    [SerializeField] private ZookeeperFollow zookeeper;
    [SerializeField] private Transform waitPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            zookeeper.GoToAndWait(waitPoint.position);
        }
    }
}
