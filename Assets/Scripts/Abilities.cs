using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour {
    public GameObject RoomGenerator;
    public GameObject cooldown;
    public Material noReflection;
    public Material Reflection;

    bool unblind = false;
    bool charged = true;
    void Start() {
        cooldown.transform.localScale = new Vector3(0, 0.5f, 0.5f);
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        if (unblind) setMaterial(Reflection);
        else setMaterial(noReflection);
        if (Mathf.Approximately(cooldown.transform.localScale.x, 0)) charged = true; else charged = false;
        if (Input.GetButtonDown("Fire2") && charged) {
            charged = false;
            StartCoroutine(ability());
        }
    }

    void setMaterial(Material material) {

        foreach (Transform room in RoomGenerator.transform) {
            Transform floorParent = room.GetChild(0);
            Transform floor = floorParent.transform.GetChild(0);
            floor.transform.GetComponent<MeshRenderer>().material = material;
        }
    }

    private IEnumerator ability() {
        unblind = true;
        charged = false;
        yield return new WaitForSeconds(4);
        cooldown.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        unblind = false;
        StartCoroutine(ScaleOverTime(cooldown, 4));
    }

    private IEnumerator ScaleOverTime(GameObject ability, float duration) {
        charged = false;
        var startScale = ability.transform.localScale;
        var endScale = new Vector3(0, ability.transform.localScale.y, ability.transform.localScale.z);
        var elapsed = 0f;

        while (elapsed < duration) {
            var t = elapsed / duration;
            ability.transform.localScale = Vector3.Lerp(startScale, endScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        ability.transform.localScale = endScale;
        charged = true;
    }
}
