using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Player")]

public class PlayerStats : ScriptableObject {
    public Stats stats = new();

    public bool recharging = false;

    //lose health
    public bool Damage (float amount) {
        if (amount > stats[StatNames.Health]) stats[StatNames.Health] = 0;
        stats[StatNames.Health] -= amount;
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
        return true;
    }

    //gain mana
    public bool Infuse (float amount) {
        if (stats[StatNames.Mana] == stats[StatNames.MaxMana]) return false;
        stats[StatNames.Mana] = Mathf.Min(stats[StatNames.Mana] + amount, stats[StatNames.MaxMana]);
        return true;
    }
}
