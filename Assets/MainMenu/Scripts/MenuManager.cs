using UnityEngine;
using UnityEngine.SceneManagement;





public class MenuManager : MonoBehaviour
{
  
    public void StartGame()
    {
        // Load your actual game scene
        SceneManager.LoadScene("Zone1Playable");
    }

    public void OpenOptions()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void GoBackMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
