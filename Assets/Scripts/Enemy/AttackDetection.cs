using System.Collections;
using UnityEngine;

//damage over time: playerStats.Damage( (enemyStats.stats[StatNames.Attack] - playerStats.stats[StatNames.Defense]) * Time.deltaTime);
public class AttackDetection : MonoBehaviour {
    public EnemyStats enemyStats;
    public PlayerStats playerStats;
    public SphereCollider sphere;
    public static bool canHit = true;
    public Animate animate;

    private void Start() {
        float scale = enemyStats.stats[StatNames.AttackRadius];
        sphere.radius = scale;
    }
    private void OnTriggerEnter(Collider c) {
        if (c.CompareTag("Environment") || c.CompareTag("Enemy")) return;
        if (c.transform.parent != null && c.transform.parent.CompareTag("Enemy")) return;
        if (c.CompareTag("Player")) {
            canHit = true;
        }
    }

    private void OnTriggerStay(Collider c) {
        if (c.CompareTag("Player") && canHit) {
            animate.Attack();
            StartCoroutine(Waiter());
        }
    }
    private IEnumerator Waiter() {
        canHit = false;
        playerStats.Damage(enemyStats.stats[StatNames.Attack] - playerStats.stats[StatNames.Defense]);
        yield return new WaitForSeconds(enemyStats.stats[StatNames.AttackInterval]);
        canHit = true;
    }

    private void OnTriggerExit(Collider c) {
        if (!animate.GetComponent<Animator>().GetBool("isDying")) animate.Reset();
        canHit = false; 
    }
}
