using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour {
    public Animator anim;
    int prevStatus = 0;
    // 0: idle; 1: patrolling; 2: chasing; 3: attacking; 4: dying; 5: dead
    void Start() {
        anim = gameObject.GetComponent<Animator>();
    }

    public void SaveStatus() {
        if(prevStatus != anim.GetInteger("status")) prevStatus = anim.GetInteger("status");
    }

    public void Patrol() {
        SaveStatus();
        anim.SetInteger("status", 1);
    }

    public void Chase() {
        SaveStatus();
        anim.SetInteger("status", 2);
    }

    public void Attack() {
        SaveStatus();
        anim.SetInteger("status", 3);
    }

    public void Die() {
        SaveStatus();
        anim.SetInteger("status", 4);
    }

    public void Reset() {
        anim.SetInteger("status", prevStatus);
    }

    public int GetStatus() {
        return anim.GetInteger("status");
    }
}
