using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour {
    public Material invisible;
    public Material visible;
    public PlayerStats playerStats;
    public SphereCollider sphere;

    private void Start() {
        float scale = playerStats.stats[StatNames.SightRadius];
        sphere.radius = scale;
    }

    private void OnTriggerEnter(Collider c) {
        if (c.CompareTag("Enemy")) {
            var enemy = c.transform;
            if (enemy.childCount == 1) enemy = enemy.parent;
            else enemy = enemy.GetChild(0);
            Debug.Log("detected " + enemy + " " + enemy.childCount);
            for (int i = 1; i < enemy.childCount; i++) {
                if(enemy.GetChild(i).TryGetComponent<Renderer>(out var renderer)) renderer.sharedMaterial = visible;
            }
        }
    }

    private void OnTriggerExit(Collider c) {
        if (c.CompareTag("Enemy")) {
            var enemy = c.transform;
            if (enemy.childCount == 1) enemy = enemy.parent;
            else enemy = enemy.GetChild(0);
            Debug.Log("detected " + enemy + " " + enemy.childCount);
            for (int i = 1; i < enemy.childCount; i++) {
                if (enemy.GetChild(i).TryGetComponent<Renderer>(out var renderer)) renderer.sharedMaterial = invisible;
            }
        }
    }
}
