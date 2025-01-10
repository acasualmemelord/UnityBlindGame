using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    public static void LoadMainMenu() {
        SceneManager.LoadScene("Main Menu");
    }

    public static void LoadSettings() {
        SceneManager.LoadScene("SettingsScene", LoadSceneMode.Additive);
    }
    public static void LoadTutorial() {
        SceneManager.LoadScene("Tutorial");
    }

    public static void LoadSampleScene() {
        SceneManager.LoadScene("SampleScene");
    }

    public static void LoadGameOver() {
        SceneManager.LoadScene("GameOverScene");
    }

    public static void LoadWinScene() {
        SceneManager.LoadScene("WinScene");
    }
}