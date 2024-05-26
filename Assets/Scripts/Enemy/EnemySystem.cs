using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemySystem : MonoBehaviour {
    public CharacterController controller;
    public GameObject player;
    public GameObject eyeLine;
    public PlayerStats playerStats;
    public EnemyStats enemyStats;
    public EnemyStats thisStats;
    public PlayerDetection playerDetection;
    public Animate animate;
    public float hp;
    public bool dying = false;
    public bool hostile = false;
    
    private void Start() {
        playerDetection = transform.GetComponentInChildren<PlayerDetection>();
        animate = transform.GetComponent<Animate>();
        hp = enemyStats.stats[StatNames.MaxHealth];
        thisStats = enemyStats;
    }

    void Update() {
        if (hp <= 0 && !dying) {
            dying = true;
            gameObject.tag = "Invisible";
            animate.Die();
            playerStats.GainMana(10);
        }
        hostile = playerDetection.found;
        if (hostile) {
            Vector3 newtarget = player.transform.position;
            newtarget.y = transform.position.y;
            transform.LookAt(newtarget);
            _ = Physics.Raycast(eyeLine.transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit);
            if (hit.collider.CompareTag("Player") || (hit.collider.transform.parent != null && hit.collider.transform.parent.CompareTag("Player"))) {
                animate.Chase();
                if (!dying) controller.SimpleMove(enemyStats.stats[StatNames.Speed] * transform.forward);
            }
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
