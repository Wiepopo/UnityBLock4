using UnityEngine;
using UnityEngine.SceneManagement;





public class MenuManager : MonoBehaviour
{
    public string startgame;
    public string options;
    public void StartGame()
    {
        // Load your actual game scene
        SceneManager.LoadScene("KamenGrayBox");
    }

    public void OpenOptions()
    {
        Debug.Log(options);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
