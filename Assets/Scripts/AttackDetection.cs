using System.Collections;
using UnityEngine;

//damage over time: playerStats.Damage( (enemyStats.stats[StatNames.Attack] - playerStats.stats[StatNames.Defense]) * Time.deltaTime);
public class AttackDetection : MonoBehaviour {
    public EnemyStats enemyStats;
    public PlayerStats playerStats;
    public GameObject sphere;
    public static bool canHit = true;
    private void Start() {
        float scale = enemyStats.stats[StatNames.AttackRadius];
        sphere.transform.localScale = new Vector3(scale, scale, scale);
    }
    private void OnTriggerEnter(Collider other) {
        canHit = true;
    }

    private void OnTriggerStay(Collider c) {
        if (c.name == "First Person Player" && canHit) {
            Debug.Log(sphere.GetInstanceID() + " entered");
            StartCoroutine(Waiter());
        }
    }
    private IEnumerator Waiter() {
        canHit = false;
        Debug.Log(enemyStats.stats[StatNames.Attack] - playerStats.stats[StatNames.Defense]);
        playerStats.Damage(enemyStats.stats[StatNames.Attack] - playerStats.stats[StatNames.Defense]);
        yield return new WaitForSeconds(enemyStats.stats[StatNames.AttackInterval]);
        canHit = true;
    }

    private void OnTriggerExit(Collider c) {
        //Debug.Log("left");
        canHit = false; 
    }
}
