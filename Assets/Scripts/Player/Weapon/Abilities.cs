using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour {
    public GameObject RoomGenerator;
    public AudioSource audioSource;

    public int currentAbility = 0;
    private bool abilChanged = true;
    public GameObject meditateParent;
    public GameObject forcefieldParent;
    public GameObject ricochetParent;
    public GameObject[] abils = { };
    public int[] abilUses = { };
    public int[] abilMaxUses = { };

    public GameObject meditateCooldown;
    public int meditateCost = 20;
    public int meditateTime = 4;
    public int meditateCooldownTime = 8;
    public int meditateUses = 0;
    public int meditateMaxUses = 5;
    bool unblind = false;
    bool meditateCharged = true;

    public GameObject forcefieldCooldown;
    public int forcefieldCost = 10;
    public int forcefieldTime = 4;
    public int forcefieldCooldownTime = 4;
    public int forcefieldUses = 0;
    public int forcefieldMaxUses = 5;
    public GameObject forcefield;
    public GameObject forcefieldPoint;
    public GameObject userCamera;
    bool forcefieldCharged = true;

    public GameObject ricochetCooldown;
    public int ricochetCost = 10;
    public int ricochetTime = 5;
    public int ricochetCooldownTime = 4;
    public int ricochetUses = 0;
    public int ricochetMaxUses = 5;
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

        userCamera = GameObject.Find("First Person Player").transform.GetChild(0).gameObject;

        playerMovement = userCamera.transform.parent.GetComponentInChildren<PlayerMovement>();

        abils = new GameObject[] { meditateParent, forcefieldParent, ricochetParent };
        abilMaxUses = new int[] { meditateMaxUses, forcefieldMaxUses, ricochetMaxUses };
    }
    void Update() {
        GameObject pauseMenu = GameObject.Find("PauseMenu");
        if (pauseMenu != null) return;
        if (unblind) SetMaterial(Reflection);
        else SetMaterial(noReflection);
        if (Mathf.Approximately(meditateCooldown.transform.localScale.x, 0)) meditateCharged = true; else meditateCharged = false;
        if (Mathf.Approximately(forcefieldCooldown.transform.localScale.x, 0)) forcefieldCharged = true; else forcefieldCharged = false;
        if (Mathf.Approximately(ricochetCooldown.transform.localScale.x, 0)) ricochetCharged = true; else ricochetCharged = false;
        
        if (Input.GetAxis("Mouse ScrollWheel") > 0) {
            abilChanged = true;
            currentAbility++;
            if (currentAbility > 2) {
                currentAbility = 0;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            abilChanged = true;
            currentAbility--;
            if (currentAbility < 0) {
                currentAbility = 2;
            }
        }
        if (abilChanged) {
            abilChanged = false;
            abilUses = new int[] { meditateUses, forcefieldUses, ricochetUses };
            for (int i = 0; i < abils.Length; i++) {
                abils[i].transform.Find("Uses").GetComponentInChildren<TextMeshProUGUI>().text = "" + (abilMaxUses[i] - abilUses[i]);
                if (i != currentAbility) {
                    RawImage image = abils[i].transform.Find("Icon").GetComponentInChildren<RawImage>();
                    var temp = image.color;
                    temp.a = 0.5f;
                    image.color = temp;
                    abils[i].transform.Find("Uses").gameObject.SetActive(false);
                } else {
                    RawImage image = abils[i].transform.Find("Icon").GetComponentInChildren<RawImage>();
                    var temp = image.color;
                    temp.a = 1f;
                    image.color = temp;
                    abils[i].transform.Find("Uses").gameObject.SetActive(true);
                }
            }
        }

        if (Input.GetButtonDown("DMed") && meditateCharged && playerStats.UseMana(meditateCost)) {
            meditateCharged = false;
            StartCoroutine(DebugMeditate());
        }

        if (Input.GetButtonDown("Fire2")) {
            if (currentAbility == 0 && meditateCharged && meditateUses <= meditateMaxUses && playerStats.UseMana(meditateCost)) {
                meditateCharged = false;
                meditateUses++;
                StartCoroutine(Meditate());
            }
            if (currentAbility == 1 && forcefieldCharged && playerStats.UseMana(forcefieldCost)) {
                forcefieldCharged = false;
                StartCoroutine(Forcefield());
            }
            if (currentAbility == 2 && ricochetCharged && ricochetUses <= ricochetMaxUses && playerStats.UseMana(ricochetCost)) {
                ricochetCharged = false;
                ricochetUses++;
                StartCoroutine(Ricochet());
            }
            abilChanged = true;
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
        audioSource.Play();
        float speed = playerStats.stats[StatNames.Speed];
        playerMovement.Speed = 0;
        yield return new WaitForSeconds(2);
        playerMovement.Speed = (int)speed;
        unblind = true;
        meditateCharged = false;
        yield return new WaitForSeconds(meditateTime);
        meditateCooldown.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        unblind = false;
        StartCoroutine(ScaleOverTime(meditateCooldown, meditateCooldownTime));
        meditateCharged = true;
    }
    private IEnumerator DebugMeditate() {
        audioSource.Play();
        float speed = playerStats.stats[StatNames.Speed];
        playerMovement.Speed = 0;
        yield return new WaitForSeconds(2);
        playerMovement.Speed = (int)speed;
        unblind = true;
        meditateCharged = false;
        yield return new WaitForSeconds(999999999);
        meditateCooldown.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        unblind = false;
        StartCoroutine(ScaleOverTime(meditateCooldown, meditateCooldownTime));
        meditateCharged = true;
    }

    private IEnumerator Forcefield() {
        forcefieldCharged = false;
        var deployedForcefield = Instantiate(forcefield, forcefieldPoint.transform.position, forcefield.transform.localRotation, null);
        yield return new WaitForSeconds(forcefieldTime);
        Destroy(deployedForcefield);
        forcefieldCooldown.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        StartCoroutine(ScaleOverTime(forcefieldCooldown, forcefieldCooldownTime));
        forcefieldCharged = true;
    }
    private IEnumerator Ricochet() {
        ricochetCharged = false;
        Attack attack = staff.GetComponentInChildren<Attack>();
        attack.projectile = ricochet;
        yield return new WaitForSeconds(ricochetTime);
        attack.projectile = original;
        ricochetCooldown.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        StartCoroutine(ScaleOverTime(ricochetCooldown, ricochetCooldownTime));
        ricochetCharged = true;
    }

    private IEnumerator ScaleOverTime(GameObject ability, float duration) {
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
    }
}
