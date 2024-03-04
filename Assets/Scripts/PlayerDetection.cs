using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour {
    public static bool found = false;

    private void OnTriggerEnter(Collider c) {
        if (c.name == "First Person Player") {
            found = true;
        }
    }

    private void OnTriggerExit(Collider c) { found = false; }
}
