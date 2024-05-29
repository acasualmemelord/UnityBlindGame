using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : MonoBehaviour {
    public Collider homingStatus;
    public SphereCollider sphere;
    public PlayerStats stats;
    public float radius;

    private void Start() {
        radius = stats.homingRadius;
        sphere.radius = stats.homingRadius;
        StartCoroutine(Shrink(10));
    }

    private void OnTriggerEnter(Collider c) {
        if (c.CompareTag("Enemy")) {
            homingStatus = c;
            StartCoroutine(Shrink(10));
        }
    }

    private void OnTriggerExit(Collider c) {
        homingStatus = null;
    }

    private IEnumerator Shrink(int ShrinkDuration) {
        float t = 0;
        float startScale = stats.homingRadius;
        float targetScale = 0;
        while(t < 1) {
            if (homingStatus != null) {
                sphere.radius = radius;
                break;
            }
            t += Time.deltaTime / ShrinkDuration;
            sphere.radius = (1 - t) * startScale + t * targetScale;
            yield return null;
        }
    }
}
