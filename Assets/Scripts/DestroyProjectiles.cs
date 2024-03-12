using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectiles : MonoBehaviour {
    
    public void OnCollisionEnter(Collision collision) {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Projectile") Destroy(collision.gameObject);
    }

    private void OnTriggerEnter(Collider c) {
        if (c.name == "Projectile") {
            Destroy(c.gameObject);
        }
    }
}
