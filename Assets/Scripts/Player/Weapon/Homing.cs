using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : MonoBehaviour {
    public Collider homingStatus;
    public SphereCollider sphere;
    public PlayerStats stats;

    private void Start() {
        sphere.radius = stats.homingRadius;
    }

    private void OnTriggerEnter(Collider c) {
        homingStatus = c;
    }

    private void OnTriggerExit(Collider c) {
        homingStatus = null;
    }
}
