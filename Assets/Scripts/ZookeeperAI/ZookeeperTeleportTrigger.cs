using UnityEngine;

public class ZookeeperTeleportTrigger : MonoBehaviour
{
    [SerializeField] private ZookeeperFollow zookeeper;
    [SerializeField] private Transform teleportTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            zookeeper.TeleportTo(teleportTarget.position);
        }
    }
}