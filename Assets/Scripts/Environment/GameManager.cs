using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    public static SoundManager soundManager;
    public static void LoadMainMenu() {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Main Menu");
    }

    public static void LoadSettings() {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("SettingsScene", LoadSceneMode.Additive);
    }
    public static void LoadTutorial() {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Tutorial");
    }

    public static void LoadSampleScene() {
        soundManager = SoundManager.Instance;
        soundManager.StopBackgroundMusic();
        SceneManager.LoadScene("SampleScene");
    }

    public static void LoadGameOver() {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("GameOverScene");
    }

    public static void LoadWinScene() {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("WinScene");
    }
}