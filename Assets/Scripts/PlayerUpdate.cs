using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpdate : MonoBehaviour
{
    public PlayerStats playerStats;
    public Transform healthBar;
    public Transform manaBar;

    void Update() {
        healthBar.localScale = new Vector3(playerStats.stats[StatNames.Health] / playerStats.stats[StatNames.MaxHealth], 0.5f, 1f);
        manaBar.localScale = new Vector3(playerStats.stats[StatNames.Mana] / playerStats.stats[StatNames.MaxMana], 0.5f, 1f);

        if (playerStats.stats[StatNames.Health] < 100) playerStats.Heal(playerStats.stats[StatNames.HealthRegen]);
        else if (playerStats.stats[StatNames.Health] > 100) playerStats.stats[StatNames.Health] = 100;

        if (playerStats.recharging) {
            if (playerStats.stats[StatNames.Mana] < 100) playerStats.stats[StatNames.Mana] += playerStats.stats[StatNames.ManaRegen];
            else if (playerStats.stats[StatNames.Mana] > 100) playerStats.stats[StatNames.Mana] = 100;
        }
    }
}
