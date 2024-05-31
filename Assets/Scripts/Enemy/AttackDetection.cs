using System.Collections;
using UnityEngine;

//damage over time: playerStats.Damage( (enemyStats.stats[StatNames.Attack] - playerStats.stats[StatNames.Defense]) * Time.deltaTime);
public class AttackDetection : MonoBehaviour {
    public EnemyStats enemyStats;
    public PlayerStats playerStats;
    private float radius;
    public bool canHit = true;
    public Animate animate;
    public LayerMask playerMask;
    private Collider[] colliders = new Collider[1];
    private bool entered = false;

    private void Start() {
        radius = enemyStats.stats[StatNames.AttackRadius];
    }

    private void Update() {
        colliders = new Collider[1];
        if (Physics.OverlapSphereNonAlloc(transform.position, radius, colliders, playerMask) > 0) {
            if (!entered) {
                entered = true;
                canHit = true;
            }
            if (canHit) {
                animate.Attack();
                StartCoroutine(Waiter());
            }
        }
        else {
            animate.Reset();
            entered = false;
            canHit = false;
        }
    }

    private IEnumerator Waiter() {
        canHit = false;
        playerStats.Damage(enemyStats.stats[StatNames.Attack] - playerStats.stats[StatNames.Defense]);
        yield return new WaitForSeconds(enemyStats.stats[StatNames.AttackInterval]);
        canHit = true;
    }

    /*
    private void OnTriggerEnter(Collider c) {
        if (c.CompareTag("Environment") || c.CompareTag("Enemy")) return;
        if (c.transform.parent != null && c.transform.parent.CompareTag("Enemy")) return;
        if (c.CompareTag("Player")) {
            Debug.Log("hi 2");
            canHit = true;
        }
    }

    private void OnTriggerStay(Collider c) {
        if (c.CompareTag("Player") && canHit) {
            Debug.Log("hi");
            animate.Attack();
            StartCoroutine(Waiter());
        }
    }
    
    private void OnTriggerExit(Collider c) {
        if (!animate.GetComponent<Animator>().GetBool("isDying")) animate.Reset();
        canHit = false; 
    }*/
}
