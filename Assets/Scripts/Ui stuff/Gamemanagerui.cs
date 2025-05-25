using UnityEngine;

public class GameManagerui : MonoBehaviour
{
    public GameObject UIWrapper;

    void Start()
    {
        // Enable the wrapper
        UIWrapper.SetActive(true);

        // Then run the initialization logic
        UIWrapper.GetComponent<UIWrapper>().InitializeUI();
    }
}