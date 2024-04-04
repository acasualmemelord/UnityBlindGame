using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Player")]

public class PlayerStats : ScriptableObject
{
    public float health = 100f;
    public float mana = 100f;
    public float healthRegen = 0.1f;
    public float manaRegen = 0.1f;
    public bool healing = false;
    public bool recharging = false;

    public bool damage (float amount) {
        if (amount > health) health = 0;
        health -= amount;
        return true;
    }

    public bool use (float amount) {
        if(amount > mana) return false;
        mana -= amount;
        return true;
    }
}
