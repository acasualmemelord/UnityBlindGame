using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour {
    public Material invisible;
    public Material visible;

    private void OnTriggerEnter(Collider c) {
        if (c.CompareTag("Enemy")) {
            var enemy = c.transform.parent;
            for (int i = 1; i < c.transform.parent.childCount - 1; i++) {
                if(enemy.GetChild(i).TryGetComponent<Renderer>(out var renderer)) renderer.sharedMaterial = visible;
            }
        }
    }

    private void OnTriggerExit(Collider c) {
        if (c.CompareTag("Enemy")) {
            var enemy = c.transform.parent;
            for (int i = 1; i < c.transform.parent.childCount - 1; i++) {
                if (enemy.GetChild(i).TryGetComponent<Renderer>(out var renderer)) renderer.sharedMaterial = invisible;
            }
        }
    }
}
