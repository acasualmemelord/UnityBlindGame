using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : MonoBehaviour {
    public Collider homingStatus;
    public GameObject homingSphere;
    public PlayerStats stats;

    private void Start() {
        float size = stats.homingRadius;
        homingSphere.transform.localScale = new Vector3(size, size, size);
    }

    private void OnTriggerEnter(Collider c) {
        homingStatus = c;
    }

    private void OnTriggerExit(Collider c) {
        homingStatus = null;
    }
}
