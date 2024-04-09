using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Loads the game scene by name
        SceneManager.LoadScene("SampleScene");
    }

    public void Settings()
    {
        // Loads the Settings menu
        SceneManager.LoadScene("SettingsScene");
    }

    public void ExitGame()
    {
        // Exits the game
        Application.Quit();
    }
}