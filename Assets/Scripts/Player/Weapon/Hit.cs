using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour {
    public Collider colliderStatus;

    private void OnTriggerEnter(Collider c) {
        //Debug.Log(colliderStatus);
        colliderStatus = c;
    }

    private void OnTriggerExit(Collider c) {
        colliderStatus = null;
    }
}
