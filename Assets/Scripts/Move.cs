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
            Debug.Log(c.name);
            if (c.name == "EnemyBody") {
                c.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().AddForce(10f * c.transform.forward);
            }
            Destroy(gameObject);
        }
    }
}
