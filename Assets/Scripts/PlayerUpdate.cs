using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpdate : MonoBehaviour
{
    public PlayerStats playerStats;
    public Transform healthBar;
    public Transform manaBar;

    void Update() {
        healthBar.localScale = new Vector3(playerStats.health / 100f, 0.5f, 1f);
        manaBar.localScale = new Vector3(playerStats.mana / 100f, 0.5f, 1f);

        if (playerStats.health < 100) playerStats.health += playerStats.healthRegen;
        else if (playerStats.health > 100) playerStats.health = 100;

        if (playerStats.recharging) {
            if (playerStats.mana < 100) playerStats.mana += playerStats.manaRegen;
            else if (playerStats.mana > 100) playerStats.mana = 100;
        }
    }
}
