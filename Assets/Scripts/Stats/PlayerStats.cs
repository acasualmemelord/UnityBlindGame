using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Player")]

public class PlayerStats : ScriptableObject {
    public Stats stats = new();

    //mage stats
    public float homingRadius = 40;
    public float attackManaCost = 1;

    public float hpCooldown = 0;
    public float maxHPCooldown = 500;
    public float manaCooldown = 0;
    public float maxManaCooldown = 500;

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
    public bool Use (float amount) {
        if(amount > stats[StatNames.Mana]) return false;
        stats[StatNames.Mana] -= amount;
        manaCooldown = maxManaCooldown;
        return true;
    }

    //gain mana
    public bool Infuse (float amount) {
        if (stats[StatNames.Mana] == stats[StatNames.MaxMana]) return false;
        stats[StatNames.Mana] = Mathf.Min(stats[StatNames.Mana] + amount, stats[StatNames.MaxMana]);
        return true;
    }
}
