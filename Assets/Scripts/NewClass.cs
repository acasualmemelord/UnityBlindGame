using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void GameOver()
    {
        // Loads the game over scene
        SceneManager.LoadScene("GameOverScene");
    }
}