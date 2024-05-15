using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    public void Restart()
    {
        GameManager.LoadSampleScene();
    }

    public void MainMenu()
    {
        GameManager.LoadMainMenu();
    }
}