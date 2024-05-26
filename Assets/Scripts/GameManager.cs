using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    public static void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public static void LoadSettings()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public static void LoadSampleScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public static void LoadGameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public static void LoadWinScene()
    {
        SceneManager.LoadScene("WinScene");
    }
}