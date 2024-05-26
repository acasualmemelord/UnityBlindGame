using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Attack : MonoBehaviour {
    public PlayerStats playerStats;

    public GameObject projectile;
    public GameObject point;
    public GameObject userCamera;

    public GameObject pauseMenu;

    public AudioSource audioSource;

<<<<<<< Updated upstream
    public GameObject pauseMenu;

    void Update()
    {
        if (!pauseMenu.activeSelf && Input.GetButtonDown("Fire1") && playerStats.UseMana(playerStats.attackManaCost))
        {
=======
    void Update(){
        if (!pauseMenu.activeSelf && Input.GetButtonDown("Fire1") && playerStats.UseMana(playerStats.attackManaCost)) {
>>>>>>> Stashed changes
            audioSource.Play();
            _ = Instantiate(projectile, point.transform.position, Quaternion.LookRotation(userCamera.transform.forward));
        }
    }
}
