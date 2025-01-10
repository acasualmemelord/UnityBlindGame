using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {
    public Button playButton;
    public Button menuButton;

    private void Start() {
        playButton.onClick.AddListener(PlayGame);
        menuButton.onClick.AddListener(ReturnToMenu);
    }

    public void PlayGame() {
        GameManager.LoadSampleScene();
    }

    public void ReturnToMenu() {
        GameManager.LoadMainMenu();
    }
}
