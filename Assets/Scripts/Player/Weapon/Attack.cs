using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Attack : MonoBehaviour {
    public PlayerStats playerStats;

    public GameObject projectile;
    public GameObject point;
    public GameObject userCamera;

    public AudioSource audioSource;

    void Update(){
        GameObject pauseMenu = GameObject.Find("PauseMenu");
        if (pauseMenu == null && Input.GetButtonDown("Fire1") && playerStats.UseMana(playerStats.attackManaCost)) {
            audioSource.Play();
            _ = Instantiate(projectile, point.transform.position, Quaternion.LookRotation(userCamera.transform.forward));
        }
    }
}
