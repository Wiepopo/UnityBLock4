using UnityEngine;

public class HideZookeeperOnTrigger : MonoBehaviour
{
    public GameObject zookeeperToHide; // Assign in Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && zookeeperToHide != null)
        {
            zookeeperToHide.SetActive(false); // Hides the zookeeper
        }
    }
}
