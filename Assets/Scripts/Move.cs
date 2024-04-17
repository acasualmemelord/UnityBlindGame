using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    public PlayerStats playerStats;
    public float speed = 100f;
    public float multiplier = 1f;
    public Rigidbody rb;
    public Hit hit;
    public Homing homing;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        hit = this.transform.GetChild(0).GetChild(0).GetComponent<Hit>();
        homing = this.transform.GetChild(0).GetChild(1).GetComponent<Homing>();
    }

    void Update() {
        Collider target = homing.homingStatus;
        if (target && target.name == "EnemyBody") {
            Vector3 distance = (target.transform.position - transform.position);
            multiplier = 1.5f;
            rb.AddForce(speed * multiplier * ((transform.forward + distance) / 2));
        }
        else {
            multiplier = 1;
            rb.AddForce(speed * multiplier * transform.forward);
        }
        Collider collider = hit.colliderStatus;
        if (collider && !collider.CompareTag("Invisible") && !collider.CompareTag("Player")) {
            if (collider.name == "EnemyBody") {
                GameObject enemy = collider.gameObject.transform.parent.gameObject;
                EnemyStats enemyStats = enemy.GetComponent<EnemySystem>().thisStats;
                enemy.GetComponent<Rigidbody>().AddForce(1000f * transform.forward);
                enemyStats.Damage(playerStats.stats[StatNames.Magic] - enemyStats.stats[StatNames.Resilience]);
                Debug.Log(enemyStats.stats[StatNames.Health] + " " + playerStats.stats[StatNames.Magic] + " " + enemyStats.stats[StatNames.Resilience]);
            }
            Destroy(gameObject);
        }
    }
}
