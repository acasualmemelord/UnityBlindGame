using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        if (c.CompareTag("Player")) {
            Debug.Log(sphere.GetInstanceID());
            found = true;
        }
    }

    private void OnTriggerExit(Collider c) { found = false; }
}
