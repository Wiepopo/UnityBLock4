using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Feedable>(out var feedable))
        {
            // You could access more data from the Feedable component here
            Debug.Log("Fed the animal!");
            Destroy(gameObject); // destroy the food item
        }
    }

}