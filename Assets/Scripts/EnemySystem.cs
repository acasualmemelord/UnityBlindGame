using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemySystem : MonoBehaviour {
    bool lookat = false;
    public GameObject player;
    Rigidbody rb;
    public EnemyStats enemyStats;
    public EnemyStats thisStats;
    public PlayerDetection playerDetection;
    public Animate animate;
    public float hp;
    public bool dying = false;
    
    private void Start() {
        rb = GetComponent<Rigidbody>();
        playerDetection = transform.parent.GetComponentInChildren<PlayerDetection>();
        animate = transform.GetComponent<Animate>();
        hp = enemyStats.stats[StatNames.MaxHealth];
        thisStats = enemyStats;
    }

    void Update() {
        if (hp <= 0 && !dying) {
            dying = true;
            animate.Die();
        }
        if (playerDetection.found) {
            lookat = true;
        }
        if (lookat) {
            Vector3 newtarget = player.transform.position;
            newtarget.y = transform.position.y;
            transform.LookAt(newtarget);
            animate.Chase();
            rb.AddForce(thisStats.stats[StatNames.Speed] * Time.deltaTime * transform.forward);
        } else if (!animate.anim.GetBool("isDying")){
            animate.Reset();
        }
    }

#pragma warning disable IDE0051 // Remove unused private members
    private void Destroy() {
        Destroy(gameObject);
    }
#pragma warning restore IDE0051 // Remove unused private members
}
