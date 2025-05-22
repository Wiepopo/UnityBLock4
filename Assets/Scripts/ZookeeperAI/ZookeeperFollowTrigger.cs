using UnityEngine;

public class ZookeeperFollowTrigger : MonoBehaviour
{
    public ZookeeperFollow zookeeperFollow;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger hit by: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger - enabling follow");
            zookeeperFollow.EnableFollow();
            gameObject.SetActive(false); // Optional: disable trigger after use
        }
    }
}
