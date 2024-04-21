using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Enemy")]

public class EnemyStats : ScriptableObject {
    public Stats stats = new();

    //lose health
    public bool Damage(float in_health,  float amount, out float health) {
        if (amount > in_health) health = 0;
        else health = in_health - Mathf.Max(amount, 1);
        return true;
    }

    //gain health
    public bool Heal(float amount) {
        if (stats[StatNames.Health] == stats[StatNames.MaxHealth]) return false;
        stats[StatNames.Health] = Mathf.Min(stats[StatNames.Health] + amount, stats[StatNames.MaxHealth]);
        return true;
    }
}
