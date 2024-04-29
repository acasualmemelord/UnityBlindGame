using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ricochet : MonoBehaviour {
    public PlayerStats playerStats;
    public float speed = 1500f;
    public float multiplier = 1f;
    public Rigidbody rb;
    public Hit hit;
    public int bounces = 3;
    private Collider prev = null;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        hit = this.transform.GetChild(0).GetChild(0).GetComponent<Hit>();
        rb.AddForce(speed * multiplier * transform.forward);
        StartCoroutine(Despawner());
    }

    void Update() {
        Collider collider = hit.colliderStatus;
        if (collider && !collider.CompareTag("Invisible") && !collider.CompareTag("Player")) {
            if (collider.name == "EnemyBody") {
                GameObject enemy = collider.gameObject.transform.parent.gameObject;
                EnemySystem system = enemy.GetComponent<EnemySystem>();
                EnemyStats enemyStats = enemy.GetComponent<EnemySystem>().thisStats;
                enemy.GetComponent<Rigidbody>().AddForce(1000f * transform.forward);
                enemyStats.Damage(system.hp, playerStats.stats[StatNames.Magic] - enemyStats.stats[StatNames.Resilience], out system.hp);
                Debug.Log(enemyStats.stats[StatNames.Health] + " " + playerStats.stats[StatNames.Magic] + " " + enemyStats.stats[StatNames.Resilience]);
            }
            else Debug.Log(collider.name);
            if (bounces == 0) Destroy(gameObject);
            else if (prev && collider != prev) {
                rb.AddForce(speed * multiplier * 0.25f * transform.forward);
                bounces--;
            }

            prev = collider;
        }
    }

    private IEnumerator Despawner() {
        yield return new WaitForSeconds(10);
        if(gameObject) Destroy(gameObject);
    }
}
