using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Enemy")]

public class EnemyStats : ScriptableObject {
    public Stats stats = new();
}
