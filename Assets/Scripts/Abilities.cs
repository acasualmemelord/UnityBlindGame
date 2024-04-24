using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Abilities : MonoBehaviour {
    public GameObject RoomGenerator;

    public GameObject meditateCooldown;
    public int meditateCost = 20;
    bool unblind = false;
    bool meditateCharged = true;

    public GameObject forcefieldCooldown;
    public int forcefieldCost = 20;
    public GameObject forcefield;
    public GameObject point;
    public GameObject userCamera;
    bool forcefieldCharged = true;

    public PlayerStats playerStats;
    public Material noReflection;
    public Material Reflection;

    void Start() {
        playerStats.stats[StatNames.Mana] = 100;
        meditateCooldown.transform.localScale = new Vector3(0, 0.5f, 0.5f);
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        if (unblind) SetMaterial(Reflection);
        else SetMaterial(noReflection);
        if (Mathf.Approximately(meditateCooldown.transform.localScale.x, 0)) meditateCharged = true; else meditateCharged = false;
        if (Input.GetButtonDown("Fire2") && meditateCharged && playerStats.UseMana(meditateCost)) {
            meditateCharged = false;
            StartCoroutine(Meditate());
        }
        if (Input.GetButtonDown("Fire2") && forcefieldCharged && playerStats.UseMana(forcefieldCost)) {
            forcefieldCharged = false;
            StartCoroutine(Forcefield());
        }
    }

    void SetMaterial(Material material) {
        foreach (Transform room in RoomGenerator.transform) {
            Transform floorParent = room.GetChild(0);
            Transform floor = floorParent.transform.GetChild(0);
            floor.transform.GetComponent<MeshRenderer>().material = material;
        }
    }

    private IEnumerator Meditate() {
        unblind = true;
        meditateCharged = false;
        yield return new WaitForSeconds(4);
        meditateCooldown.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        unblind = false;
        StartCoroutine(ScaleOverTime(meditateCooldown, 4));
    }

    private IEnumerator Forcefield() {
        var deployedForcefield = Instantiate(forcefield, point.transform.position, forcefield.transform.localRotation, null);
        yield return new WaitForSeconds(4);
        Destroy(deployedForcefield);
        forcefieldCharged = true;
    }

    //todo: fast projectile that has ricochets but has no homing
    private IEnumerator ScaleOverTime(GameObject ability, float duration) {
        meditateCharged = false;
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
        meditateCharged = true;
    }
}
