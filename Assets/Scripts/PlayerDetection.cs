using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour {
    public EnemyStats enemyStats;
    public GameObject sphere;
    public static bool found = false;
    private void Start() {
        float scale = enemyStats.stats[StatNames.SightRadius];
        sphere.transform.localScale = new Vector3(scale, scale, scale);
    }

    private void OnTriggerEnter(Collider c) {
        if (c.name == "First Person Player") {
            found = true;
        }
    }

    private void OnTriggerExit(Collider c) { found = false; }
}
