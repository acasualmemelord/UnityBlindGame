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
    public GameObject forcefieldPoint;
    public GameObject userCamera;
    bool forcefieldCharged = true;

    public GameObject ricochetCooldown;
    public int ricochetCost = 10;
    public GameObject ricochet;
    public GameObject original;
    public GameObject staff;
    public GameObject ricochetPoint;
    bool ricochetCharged = true;

    public PlayerStats playerStats;
    public PlayerMovement playerMovement;
    public Material noReflection;
    public Material Reflection;

    void Start() {
        meditateCooldown.transform.localScale = new Vector3(0, 0.5f, 0.5f);
        forcefieldCooldown.transform.localScale = new Vector3(0, 0.5f, 0.5f);
        ricochetCooldown.transform.localScale = new Vector3(0, 0.5f, 0.5f);

        playerMovement = userCamera.transform.parent.GetComponentInChildren<PlayerMovement>();
    }

    void Update() {
        if (unblind) SetMaterial(Reflection);
        else SetMaterial(noReflection);
        if (Mathf.Approximately(meditateCooldown.transform.localScale.x, 0)) meditateCharged = true; else meditateCharged = false;
        if (Mathf.Approximately(forcefieldCooldown.transform.localScale.x, 0)) forcefieldCharged = true; else forcefieldCharged = false;
        if (Mathf.Approximately(ricochetCooldown.transform.localScale.x, 0)) ricochetCharged = true; else ricochetCharged = false;
        if (Input.GetButtonDown("Ability 1") && meditateCharged && playerStats.UseMana(meditateCost)) {
            meditateCharged = false;
            StartCoroutine(Meditate());
        }
        if (Input.GetButtonDown("Ability 2") && forcefieldCharged && playerStats.UseMana(forcefieldCost)) {
            forcefieldCharged = false;
            Debug.Log("e pressed");
            StartCoroutine(Forcefield());
        }
        if (Input.GetButtonDown("Ability 3") && ricochetCharged && playerStats.UseMana(ricochetCost)) {
            ricochetCharged = false;
            StartCoroutine(Ricochet());
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
        float speed = playerStats.stats[StatNames.Speed];
        playerMovement.speed = 0;
        yield return new WaitForSeconds(2);
        playerMovement.speed = speed;
        unblind = true;
        meditateCharged = false;
        yield return new WaitForSeconds(4);
        meditateCooldown.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        unblind = false;
        StartCoroutine(ScaleOverTime(meditateCooldown, 4));
        meditateCharged = true;
    }

    private IEnumerator Forcefield() {
        forcefieldCharged = false;
        var deployedForcefield = Instantiate(forcefield, forcefieldPoint.transform.position, forcefield.transform.localRotation, null);
        yield return new WaitForSeconds(4);
        Destroy(deployedForcefield);
        forcefieldCooldown.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        StartCoroutine(ScaleOverTime(forcefieldCooldown, 4));
        forcefieldCharged = true;
    }
    private IEnumerator Ricochet() {
        ricochetCharged = false;
        Attack attack = staff.GetComponentInChildren<Attack>();
        attack.projectile = ricochet;
        yield return new WaitForSeconds(5);
        attack.projectile = original;
        ricochetCooldown.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        StartCoroutine(ScaleOverTime(ricochetCooldown, 4));
        ricochetCharged = true;
    }

    private IEnumerator ScaleOverTime(GameObject ability, float duration) {
        var startScale = ability.transform.localScale;
        Debug.Log(startScale);
        var endScale = new Vector3(0, ability.transform.localScale.y, ability.transform.localScale.z);
        var elapsed = 0f;

        while (elapsed < duration) {
            var t = elapsed / duration;
            ability.transform.localScale = Vector3.Lerp(startScale, endScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        ability.transform.localScale = endScale;
    }
}
