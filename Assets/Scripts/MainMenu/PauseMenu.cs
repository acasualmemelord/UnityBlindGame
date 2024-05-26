using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button resumeButton;
    public Button settingsButton;
    public Button restartButton;
    public Button exitButton;
   

    private void Start()
    {
        resumeButton.onClick.AddListener(ResumeGame);
        settingsButton.onClick.AddListener(OpenSettings);
        restartButton.onClick.AddListener(RestartGame);
        exitButton.onClick.AddListener(ExitGame);
    }

    public void ResumeGame()
    {
        SceneManager.UnloadSceneAsync("PauseMenu");
    }

    public void OpenSettings()
    {
        GameManager.LoadSettings();
    }
    public void RestartGame()
    {
        GameManager.LoadSampleScene();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}