using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    public GameObject projectile;
    public GameObject point;
    public GameObject userCamera;

    void Update(){
        if (Input.GetButtonDown("Fire1")) {
            GameObject newProjectile = Instantiate(projectile, 
                                                   point.transform.position,
                                                   Quaternion.LookRotation(userCamera.transform.forward));
        }
    }
}
