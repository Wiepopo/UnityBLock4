using UnityEngine;
using UnityEngine.UI;

public class DestroyOnCollisiontest : MonoBehaviour
{
    public GameObject theText;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Feedable>(out var feedable))
        {
            Debug.Log("Fed the animal!");
            Destroy(gameObject); // destroy the food item

            if (GameManager.Instance == null)
            {
                Debug.LogError("GameManager.Instance is NULL. Add GameManager to the scene.");
                return;
            }

            if (GameManager.Instance.Interactions < GameManager.Instance.MaxInteractions)
            {
                GameManager.Instance.Interactions++;
                Debug.Log("Interaction + 1: " + GameManager.Instance.Interactions);

                if (GameManager.Instance.Interactions == GameManager.Instance.MaxInteractions)
                {
                    Debug.Log("Objective Completed");
                    theText.GetComponent<Text>().text = "Inspect the zoo, <color=green>Feed</color> and take photo's of the animals";
                }
            }
        }
    }
}