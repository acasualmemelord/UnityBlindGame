using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour {
    bool lookat = false;
    public GameObject player;
    Rigidbody rb;
    public float speed = 3f;
    public float multiplier = 1f;
    public float speedLimit = 2f;
    
    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        if (PlayerDetection.found) {
            lookat = true;
        }
        if (lookat) {
            transform.LookAt(player.transform);
            Vector3 v = rb.velocity;
            Debug.Log(v);
            if(!PlayerDetection.found && v.x > -2 && v.x < 2 && v.z > -2 && v.z < 2) {
                rb.AddForce(speed * multiplier * Time.deltaTime * transform.forward);
            }
        }
    }
}
