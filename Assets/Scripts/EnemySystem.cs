using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemySystem : MonoBehaviour {
    bool lookat = false;
    public GameObject player;
    Rigidbody rb;
    public EnemyStats enemyStats;
    
    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        if (enemyStats.stats[StatNames.Health] <= 0) Destroy(gameObject);
        if (PlayerDetection.found) {
            lookat = true;
        }
        if (lookat) {
            transform.LookAt(player.transform);
            Vector3 v = rb.velocity;
            if(!PlayerDetection.found && v.x > -enemyStats.stats[StatNames.Speed] && v.x < enemyStats.stats[StatNames.Speed] && v.z > -enemyStats.stats[StatNames.Speed] && v.z < enemyStats.stats[StatNames.Speed]) {
                rb.AddForce(enemyStats.stats[StatNames.Speed] * Time.deltaTime * transform.forward);
            }
        }
    }
}
