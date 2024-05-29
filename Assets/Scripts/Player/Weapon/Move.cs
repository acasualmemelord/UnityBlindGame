using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    public PlayerStats playerStats;
    public float speed = 50f;
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
        StartCoroutine(Despawner());
    }

    void Update() {
        if(!foundTarget) target = homing.homingStatus;
        if (target && target.CompareTag("Enemy"))
        {
            foundTarget = true;
            multiplier = 2f;
            Vector3 distance = Vector3.Normalize(target.transform.GetChild(0).position - transform.position) * multiplier;
            
            rb.AddForce((transform.forward + distance) / 2 * (speed * multiplier));
            Debug.Log("homing: " + (transform.forward + distance) / 2 * (speed * multiplier));
        }
        else
        {
            foundTarget = false;
            multiplier = 1;
            rb.AddForce(transform.forward * (speed * multiplier));
        }
        Collider collider = hit.colliderStatus;
        if (collider && !collider.CompareTag("Invisible") && !collider.CompareTag("Player")) {
            if (collider.CompareTag("Enemy")) {
                GameObject enemy = collider.gameObject;
                EnemySystem system;
                if(enemy.name == "Hitbox") system = enemy.GetComponentInParent<EnemySystem>();
                else system = enemy.GetComponentInChildren<EnemySystem>();
                EnemyStats enemyStats = system.thisStats;
                enemyStats.Damage(system.hp, playerStats.stats[StatNames.Magic] - enemyStats.stats[StatNames.Resilience], out system.hp);
                system.hostile = true;
                //Debug.Log(system.hp + " " + playerStats.stats[StatNames.Magic] + " " + enemyStats.stats[StatNames.Resilience]);
            }
            Destroy(gameObject);
        }
    }

    private IEnumerator Despawner() {
        yield return new WaitForSeconds(2);
        if (gameObject) Destroy(gameObject);
    }
}
