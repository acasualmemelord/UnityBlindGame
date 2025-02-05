using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winpoint : MonoBehaviour {
    private void OnTriggerEnter(Collider c) {
        if (c.CompareTag("Player")) {
            GameManager.LoadWinScene();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
