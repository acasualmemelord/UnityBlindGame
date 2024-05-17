using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour {
    public Animator anim;
    // Start is called before the first frame update
    void Start() {
        anim = gameObject.GetComponent<Animator>();
        Reset();
    }

    public void Chase() {
        anim.SetBool("isChasing", true);
    }

    public void Attack() {
        anim.SetBool("isAttacking", true);
    }

    public void Die() {
        anim.SetBool("isDying", true);
        //anim.SetBool("isDead", true);
    }

    public void Reset() {
        anim.SetBool("isAttacking", false);
        anim.SetBool("isChasing", false);
        anim.SetBool("isDying", false);
        anim.SetBool("isDead", false);
    }
}
