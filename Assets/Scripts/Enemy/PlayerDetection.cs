using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerDetection : MonoBehaviour {
    public EnemyStats enemyStats;
    public bool found = false;
    private float radius;
    public LayerMask playerMask;
    private Collider[] colliders = new Collider[1];

    private void Start() {
        radius = enemyStats.stats[StatNames.SightRadius];
    }
    
    private void Update() {
        colliders = new Collider[1];
        if(Physics.OverlapSphereNonAlloc(transform.position, radius, colliders, playerMask) > 0) {
            found = true;
        } else {
            found = false;
        }
    }
}
