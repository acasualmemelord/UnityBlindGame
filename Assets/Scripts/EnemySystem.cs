using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemySystem : MonoBehaviour {
    public CharacterController controller;
    bool lookat = false;
    public GameObject player;
    public EnemyStats enemyStats;
    public EnemyStats thisStats;
    public PlayerDetection playerDetection;
    public Animate animate;
    public float hp;
    public bool dying = false;
    
    private void Start() {
        playerDetection = transform.GetComponentInChildren<PlayerDetection>();
        animate = transform.GetComponent<Animate>();
        hp = enemyStats.stats[StatNames.MaxHealth];
        thisStats = enemyStats;
    }

    void Update() {
        if (hp <= 0 && !dying) {
            dying = true;
            animate.Die();
        }
        lookat = playerDetection.found;
        if (lookat) {
            Vector3 newtarget = player.transform.position;
            newtarget.y = transform.position.y;
            transform.LookAt(newtarget);
            animate.Chase();

            //Debug.Log(enemyStats.stats[StatNames.Speed] + " " + transform.forward);

            controller.SimpleMove(enemyStats.stats[StatNames.Speed] * transform.forward);
        }
        else if (!animate.anim.GetBool("isDying")) {
            animate.Reset();
        }
    }

#pragma warning disable IDE0051 // Remove unused private members
    private void Destroy() {
        Destroy(gameObject);
    }
#pragma warning restore IDE0051 // Remove unused private members
}
