using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    public PlayerStats playerStats;

    public GameObject projectile;
    public GameObject point;
    public GameObject userCamera;

    public AudioSource audioSource;

    void Update(){
        if (Input.GetButtonDown("Fire1") && playerStats.UseMana(playerStats.attackManaCost)) {
            audioSource.Play();
            _ = Instantiate(projectile, point.transform.position, Quaternion.LookRotation(userCamera.transform.forward));
        }
    }
}
