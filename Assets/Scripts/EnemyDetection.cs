using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour {
    public static bool found = false;

    private void OnTriggerEnter(Collider c) {
        if (c.name == "EnemyBody") {
            c.gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    private void OnTriggerExit(Collider c) {
        if (c.name == "EnemyBody") {
            c.gameObject.GetComponent<Renderer>().material.color = Color.black;
        }
    }
}
