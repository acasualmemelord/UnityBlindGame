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
    public bool foundTarget = false;
    Collider target;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        hit = this.transform.GetChild(0).GetChild(0).GetComponent<Hit>();
        homing = this.transform.GetChild(0).GetChild(1).GetComponent<Homing>();
    }

    void Update() {
        if(!foundTarget) target = homing.homingStatus;
        if (target && target.CompareTag("Enemy")) {
            foundTarget = true;
            Vector3 distance = (target.transform.GetChild(0).position - transform.position);
            multiplier = 1.5f;
            rb.AddForce(speed * multiplier * ((transform.forward + distance) / 2));
        }
        else {
            foundTarget = false;
            multiplier = 1;
            rb.AddForce(speed * multiplier * transform.forward);
        }
        Collider collider = hit.colliderStatus;
        if (collider && !collider.CompareTag("Invisible") && !collider.CompareTag("Player")) {
            if (collider.CompareTag("Enemy")) {
                GameObject enemy = collider.gameObject.transform.parent.gameObject;
                EnemySystem system = enemy.GetComponent<EnemySystem>();
                EnemyStats enemyStats = enemy.GetComponent<EnemySystem>().thisStats;
                enemy.GetComponent<Rigidbody>().AddForce(1000f * transform.forward);
                enemyStats.Damage(system.hp, playerStats.stats[StatNames.Magic] - enemyStats.stats[StatNames.Resilience], out system.hp);
                Debug.Log(system.hp + " " + playerStats.stats[StatNames.Magic] + " " + enemyStats.stats[StatNames.Resilience]);
            }
            Destroy(gameObject);
        }
    }
}
