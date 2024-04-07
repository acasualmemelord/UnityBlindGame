using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    public PlayerStats playerStats;
    public float speed = 100f;
    public float multiplier = 1f;
    public Rigidbody rb;
    public Hit hit;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        hit = this.transform.GetChild(0).GetChild(0).GetComponent<Hit>();
    }

    void Update() {
        rb.AddForce(speed * multiplier * transform.forward);
        Collider c = hit.colliderStatus;
        if (c && !c.CompareTag("Invisible") && !c.CompareTag("Player")) {
            if (c.name == "EnemyBody") {
                GameObject enemy = c.gameObject.transform.parent.gameObject;
                EnemyStats enemyStats = enemy.GetComponent<EnemySystem>().enemyStats;
                enemy.GetComponent<Rigidbody>().AddForce(1000f * transform.forward);
                enemyStats.Damage(playerStats.stats[StatNames.Magic] - enemyStats.stats[StatNames.Resilience]);
                Debug.Log(enemyStats.stats[StatNames.Health]);
            }
            Destroy(gameObject);
        }
    }
}
