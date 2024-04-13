using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Enemy")]

public class EnemyStats : ScriptableObject {
    public Stats stats = new();

    //lose health
    public bool Damage(float amount) {
        if (amount > stats[StatNames.Health]) stats[StatNames.Health] = 0;
        stats[StatNames.Health] -= Mathf.Max(amount, 1);
        return true;
    }

    //gain health
    public bool Heal(float amount) {
        if (stats[StatNames.Health] == stats[StatNames.MaxHealth]) return false;
        stats[StatNames.Health] = Mathf.Min(stats[StatNames.Health] + amount, stats[StatNames.MaxHealth]);
        return true;
    }
}
