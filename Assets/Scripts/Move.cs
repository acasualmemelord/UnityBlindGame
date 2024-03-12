using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    public float speed = 100f;
    public float multiplier = 1f;
    public Rigidbody rb;
    public Hit hit;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        hit = this.transform.GetChild(0).GetChild(0).GetComponent<Hit>();
    }

    // Update is called once per frame
    void Update() {
        rb.AddForce(speed * multiplier * transform.forward);
        Collider c = hit.colliderStatus;
        if (c && c.tag != "Invisible" && c.tag != "Player") {
            if (c.name == "EnemyBody") {
                GameObject enemy = c.gameObject.transform.parent.gameObject;
                enemy.GetComponent<Rigidbody>().AddForce(1000f * transform.forward);
                enemy.GetComponent<EnemySystem>().hp -= 25;
                Debug.Log(enemy.GetComponent<EnemySystem>().hp);
            }
            Destroy(gameObject);
        }
    }
}
