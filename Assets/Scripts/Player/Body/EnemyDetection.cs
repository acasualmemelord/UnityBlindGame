using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyDetection : MonoBehaviour {
    public Material invisible;
    public Material visible;
    public PlayerStats playerStats;
    private float radius;
    public LayerMask enemyMask;
    private readonly Collider[] colliders = new Collider[50];
    private HashSet<Transform> transforms = new();

    private void Start() {
        radius = playerStats.stats[StatNames.SightRadius];
    }
    
    private void Update() {
        var hits = Physics.OverlapSphereNonAlloc(transform.position, radius, colliders, enemyMask);
        var newColls = new HashSet<Transform>();
        for (int i = 0; i < hits; i++) {
            newColls.Add(colliders[i].transform);
            var enemy = colliders[i].transform;
            if (enemy.childCount == 1) enemy = enemy.parent;
            else enemy = enemy.GetChild(0);
            for (int j = 1; j < enemy.childCount; j++) {
                if (enemy.GetChild(j).TryGetComponent<Renderer>(out var renderer)) renderer.sharedMaterial = visible;
            }
        }
        foreach (Transform t in transforms) {
            if(t == null) {
                transforms.Remove(t);
                break;
            }
            if (!newColls.Contains(t)) {
                var enemy = t;
                if (enemy.childCount == 1) enemy = enemy.parent;
                else enemy = enemy.GetChild(0);
                for (int i = 1; i < enemy.childCount; i++) {
                    if (enemy.GetChild(i).TryGetComponent<Renderer>(out var renderer)) renderer.sharedMaterial = invisible;
                }
                newColls.Remove(t);
            }
        }
        transforms = newColls;
    }
}
