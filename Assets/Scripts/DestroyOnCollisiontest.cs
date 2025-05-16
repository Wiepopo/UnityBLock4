using UnityEngine;

public class DestroyOnCollisiontest : MonoBehaviour
{
    public int Interactions = 0;
    public int Maxinteractions = 3;


private void OnCollisionEnter(Collision collision)
    {
    if (collision.gameObject.TryGetComponent<Feedable>(out var feedable))
     {
        Debug.Log("Fed the animal!");
        Destroy(gameObject); // destroy the food item

        if (GameManager.Instance.Interactions < GameManager.Instance.MaxInteractions)
        {
            GameManager.Instance.Interactions++;
            Debug.Log("Interaction + 1: " + GameManager.Instance.Interactions);

            if (GameManager.Instance.Interactions == GameManager.Instance.MaxInteractions)
            {
                Debug.Log("Objective Completed");
            }
        }
     }
    }
}