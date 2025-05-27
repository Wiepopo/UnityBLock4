using UnityEngine;


public class Door : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.Interactions == GameManager.Instance.MaxInteractions)
        {
            Destroy(gameObject);
        }
    }

}
