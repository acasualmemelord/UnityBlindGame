using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
    private float hostileCounter = 0f;
    private float hostileTime = 5f;
    public LayerMask mask;
    
    private void Start() {
        playerDetection = transform.GetComponentInChildren<PlayerDetection>();
        animate = transform.GetComponent<Animate>();
        hp = enemyStats.stats[StatNames.MaxHealth];
        thisStats = enemyStats;
    }

    void Update() {
        if (hp <= 0 && animate.GetStatus() != 4) {
            dying = true;
            gameObject.tag = "Invisible";
            animate.Die();
            playerStats.GainMana(10);
        }
        if (!hostile) {
            if (animate.GetStatus() != 4) animate.Reset();
            hostile = playerDetection.found;
        }
        else {
            hostileCounter += Time.deltaTime;
            if (hostileCounter >= hostileTime && !playerDetection.found) {
                hostile = false;
                hostileCounter = 0f;
            } else {
                animate.Chase();
                //Debug.Log(transform.name + ": " + animate.anim.GetInteger("status"));
                Vector3 newtarget = player.transform.position;
                newtarget.y = transform.position.y;
                transform.LookAt(newtarget);
                bool lineOfSight = Physics.Raycast(eyeLine.transform.position, transform.forward, out RaycastHit hit, enemyStats.stats[StatNames.SightRadius], mask);
                //Debug.Log(transform.name + ": " + lineOfSight);
                if (hit.collider != null && (hit.collider.CompareTag("Player") || (hit.collider.transform.parent != null && hit.collider.transform.parent.CompareTag("Player")))) {
                    if (!dying) controller.SimpleMove(enemyStats.stats[StatNames.Speed] * transform.forward);
                }
            }
        }
    }

#pragma warning disable IDE0051
    private void Destroy() {
        Destroy(gameObject);
    }
#pragma warning restore IDE0051
}
