using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    public void Restart()
    {
        // Loads the sample game scene
        SceneManager.LoadScene("SampleScene");
    }

    public void MainMenu()
    {
        // Loads the main menu scene
        SceneManager.LoadScene("MainMenuScene");
    }
}