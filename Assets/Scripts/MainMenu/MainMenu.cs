using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button settingsButton;
    public Button exitButton;

    private void Start() {
        Cursor.lockState = CursorLockMode.None;
        playButton.onClick.AddListener(PlayGame);
        //settingsButton.onClick.AddListener(OpenSettings);
        exitButton.onClick.AddListener(ExitGame);

        Debug.Log(SceneManager.GetActiveScene().name);
    }

    public void PlayGame() {
        GameManager.LoadTutorial();
    }

    public void OpenSettings() {
        SceneManager.LoadScene("SettingsScene", LoadSceneMode.Additive);
    }

    public void ExitGame() {
        Application.Quit();
    }
}