using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerDetection : MonoBehaviour {
    public EnemyStats enemyStats;
    public SphereCollider sphere;
    public bool found = false;

    private void Start() {
        float scale = enemyStats.stats[StatNames.SightRadius];
        sphere.radius = scale;
    }

    private void OnTriggerEnter(Collider c) {
        if (c.CompareTag("Player")) {
            found = true;
        }
    }

    private void OnTriggerExit(Collider c) {
        if (c.CompareTag("Player")) {
            found = false;
        }
    }
}
