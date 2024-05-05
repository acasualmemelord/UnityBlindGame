using System.Collections;
using UnityEngine;

//damage over time: playerStats.Damage( (enemyStats.stats[StatNames.Attack] - playerStats.stats[StatNames.Defense]) * Time.deltaTime);
public class AttackDetection : MonoBehaviour {
    public EnemyStats enemyStats;
    public PlayerStats playerStats;
    public GameObject sphere;
    public static bool canHit = true;
    public Animate animate;

    private void Start() {
        animate = transform.parent.GetComponentInChildren<Animate>();
        float scale = enemyStats.stats[StatNames.AttackRadius];
        sphere.transform.localScale = new Vector3(scale, scale, scale);
    }
    private void OnTriggerEnter(Collider other) {
        canHit = true;
    }

    private void OnTriggerStay(Collider c) {
        if (c.name == "First Person Player" && canHit) {
            Debug.Log(sphere.GetInstanceID() + " entered");
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
