using UnityEngine;
using UnityEngine.UI;

public class PauseButtons : MonoBehaviour {
    public Button resumeButton;
    public Button settingsButton;
    public Button restartButton;
    public Button exitButton;

    public GameObject pauseMenu;

    private void Start() {
        resumeButton.onClick.AddListener(ResumeGame);
        settingsButton.onClick.AddListener(OpenSettings);
        restartButton.onClick.AddListener(RestartGame);
        exitButton.onClick.AddListener(ExitGame);
    }

    public void ResumeGame() {
        pauseMenu.SetActive(false);
    }

    public void OpenSettings() {
        GameManager.LoadSettings();
    }

    public void RestartGame() {
        GameManager.LoadSampleScene();
    }

    public void ExitGame() {
        Application.Quit();
    }
}