using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Player")]

public class PlayerStats : ScriptableObject {
    public Stats stats = new();

    public int xp = 0;
    public int level = 1;

    //movement
    public bool isSprinting = false;

    //mage stats
    public float homingRadius = 40;
    public float attackManaCost = 1;

    public float hpCooldown = 0;
    public float maxHPCooldown = 500;
    public float manaCooldown = 0;
    public float maxManaCooldown = 500;
    public float staminaCooldown = 0;
    public float maxStaminaCooldown = 250;

    //lose health
    public bool Damage (float amount) {
        if (amount > stats[StatNames.Health]) stats[StatNames.Health] = 0;
        stats[StatNames.Health] -= Mathf.Max(amount, 1);
        hpCooldown = maxHPCooldown;
        return true;
    }

    //gain health
    public bool Heal (float amount) {
        if (stats[StatNames.Health] == stats[StatNames.MaxHealth]) return false;
        stats[StatNames.Health] = Mathf.Min(stats[StatNames.Health] + amount, stats[StatNames.MaxHealth]);
        return true;
    }

    //lose mana
    public bool UseMana (float amount) {
        if(amount > stats[StatNames.Mana]) return false;
        stats[StatNames.Mana] -= amount;
        manaCooldown = maxManaCooldown;
        return true;
    }

    //gain mana
    public bool GainMana (float amount) {
        if (stats[StatNames.Mana] == stats[StatNames.MaxMana]) return false;
        stats[StatNames.Mana] = Mathf.Min(stats[StatNames.Mana] + amount, stats[StatNames.MaxMana]);
        return true;
    }

    //lose stamina
    public bool UseStamina(float amount) {
        if (amount > stats[StatNames.Stamina]) return false;
        stats[StatNames.Stamina] -= amount;
        staminaCooldown = maxStaminaCooldown;
        return true;
    }

    //gain mana
    public bool GainStamina(float amount) {
        if (stats[StatNames.Stamina] == stats[StatNames.MaxStamina]) return false;
        stats[StatNames.Stamina] = Mathf.Min(stats[StatNames.Stamina] + amount, stats[StatNames.MaxStamina]);
        return true;
    }
}
