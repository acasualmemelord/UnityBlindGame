using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpdate : MonoBehaviour {
    public PlayerStats playerStats;
    public Transform healthBar;
    public Transform manaBar;

    void Update() {
        healthBar.localScale = new Vector3(playerStats.stats[StatNames.Health] / playerStats.stats[StatNames.MaxHealth], 0.5f, 1f);
        manaBar.localScale = new Vector3(playerStats.stats[StatNames.Mana] / playerStats.stats[StatNames.MaxMana], 0.5f, 1f);

        if (playerStats.hpCooldown == 0) {
            if (playerStats.stats[StatNames.Health] < playerStats.stats[StatNames.MaxHealth]) playerStats.Heal(playerStats.stats[StatNames.HealthRegen] * Time.deltaTime);
            else if (playerStats.stats[StatNames.Health] > playerStats.stats[StatNames.MaxHealth]) playerStats.stats[StatNames.Health] = playerStats.stats[StatNames.MaxHealth];
        }
        else {
            StartCoroutine(HPWaiter());
        }

        if (playerStats.manaCooldown == 0) {
            if (playerStats.stats[StatNames.Mana] < playerStats.stats[StatNames.MaxMana]) playerStats.Infuse(playerStats.stats[StatNames.ManaRegen] * Time.deltaTime);
            else if (playerStats.stats[StatNames.Mana] > playerStats.stats[StatNames.MaxMana]) playerStats.stats[StatNames.Mana] = playerStats.stats[StatNames.MaxMana];
        }
        else {
            StartCoroutine(ManaWaiter());
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
}
