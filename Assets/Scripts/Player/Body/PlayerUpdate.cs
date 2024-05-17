using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpdate : MonoBehaviour {
    public PlayerStats playerStats;
    public Transform healthBar;
    public Transform manaBar;
    public Transform staminaBar;
    public GameManager gameManager;
    public AudioSource audioSource;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        playerStats.stats[StatNames.Health] = playerStats.stats[StatNames.MaxHealth];
        playerStats.stats[StatNames.Mana] = playerStats.stats[StatNames.MaxMana];
        playerStats.stats[StatNames.Stamina] = playerStats.stats[StatNames.MaxStamina];
        playerStats.hpCooldown = 0;
        playerStats.manaCooldown = 0;
        playerStats.staminaCooldown = 0;
    }

    void Update() {
        healthBar.localScale = new Vector3(playerStats.stats[StatNames.Health] / playerStats.stats[StatNames.MaxHealth], 0.5f, 1f);
        manaBar.localScale = new Vector3(playerStats.stats[StatNames.Mana] / playerStats.stats[StatNames.MaxMana], 1f, 1f);
        staminaBar.localScale = new Vector3(playerStats.stats[StatNames.Stamina] / playerStats.stats[StatNames.MaxStamina], 1f, 1f);

        if (playerStats.hpCooldown == 0) {
            if (playerStats.stats[StatNames.Health] < playerStats.stats[StatNames.MaxHealth]) playerStats.Heal(playerStats.stats[StatNames.HealthRegen] * Time.deltaTime);
            else if (playerStats.stats[StatNames.Health] > playerStats.stats[StatNames.MaxHealth]) playerStats.stats[StatNames.Health] = playerStats.stats[StatNames.MaxHealth];
        }
        else {
            StartCoroutine(HPWaiter());
        }

        if (playerStats.manaCooldown == 0) {
            if (playerStats.stats[StatNames.Mana] < playerStats.stats[StatNames.MaxMana]) playerStats.GainMana(playerStats.stats[StatNames.ManaRegen] * Time.deltaTime);
            else if (playerStats.stats[StatNames.Mana] > playerStats.stats[StatNames.MaxMana]) playerStats.stats[StatNames.Mana] = playerStats.stats[StatNames.MaxMana];
        }
        else {
            StartCoroutine(ManaWaiter());
        }

        if (playerStats.staminaCooldown == 0) {
            if (playerStats.stats[StatNames.Stamina] < playerStats.stats[StatNames.MaxStamina]) playerStats.GainStamina(playerStats.stats[StatNames.StaminaRegen] * Time.deltaTime);
            else if (playerStats.stats[StatNames.Stamina] > playerStats.stats[StatNames.MaxStamina]) playerStats.stats[StatNames.Stamina] = playerStats.stats[StatNames.MaxStamina];
        }
        else {
            StartCoroutine(StaminaWaiter());
        }

        if (playerStats.stats[StatNames.Health] <= 0) {
            audioSource.Play();
            GameManager.LoadGameOver();
        }
    }
    private IEnumerator HPWaiter() {
        while (playerStats.hpCooldown > 0) {
            yield return null;
            playerStats.hpCooldown -= Time.deltaTime;
        }
        playerStats.hpCooldown = 0;
    }
    private IEnumerator ManaWaiter() {
        while (playerStats.manaCooldown > 0) {
            yield return null;
            playerStats.manaCooldown -= Time.deltaTime;
        }
        playerStats.manaCooldown = 0;
    }

    private IEnumerator StaminaWaiter() {
        while (playerStats.staminaCooldown > 0) {
            yield return null;
            playerStats.staminaCooldown -= Time.deltaTime;
        }
        playerStats.staminaCooldown = 0;
    }
}
