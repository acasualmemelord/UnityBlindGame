using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemySystem : MonoBehaviour {
    bool lookat = false;
    public GameObject player;
    Rigidbody rb;
    public EnemyStats enemyStats;
    public EnemyStats thisStats;
    
    private void Start() {
        rb = GetComponent<Rigidbody>();
        enemyStats.stats[StatNames.Health] = enemyStats.stats[StatNames.MaxHealth];
        thisStats = enemyStats;
    }

    void Update() {
        if (thisStats.stats[StatNames.Health] <= 0) Destroy(gameObject);
        if (PlayerDetection.found) {
            lookat = true;
        }
        if (lookat) {
            transform.LookAt(player.transform);
            Vector3 v = rb.velocity;
            if(!PlayerDetection.found && v.x > -thisStats.stats[StatNames.Speed] && v.x < thisStats.stats[StatNames.Speed] && v.z > -thisStats.stats[StatNames.Speed] && v.z < thisStats.stats[StatNames.Speed]) {
                rb.AddForce(thisStats.stats[StatNames.Speed] * Time.deltaTime * transform.forward);
            }
        }
    }
}
