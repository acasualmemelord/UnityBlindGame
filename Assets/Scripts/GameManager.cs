using UnityEngine;
using UnityEngine.SceneManagement;

<<<<<<< Updated upstream
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
=======
public class GameManager : MonoBehaviour {
    public GameObject pauseMenu;

    private void Start() {
        pauseMenu.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if(!pauseMenu.activeSelf) {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            } else {
>>>>>>> Stashed changes
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

<<<<<<< Updated upstream
    public static void LoadMainMenu()
    {
=======
    public static void LoadMainMenu() {
>>>>>>> Stashed changes
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