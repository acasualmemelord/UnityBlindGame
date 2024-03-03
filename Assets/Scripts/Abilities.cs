using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour {
    public GameObject ground;
    Material material;

    bool unblind = false;
    void Start() {
        material = ground.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire1") && !unblind) StartCoroutine(ability());
        if(unblind) material.color = Color.white;
        else material.color = Color.black;
    }

    private IEnumerator ability() {
        unblind = true;
        yield return new WaitForSeconds(4);
        unblind = false;
    }
}
