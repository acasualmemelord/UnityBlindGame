using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stats : GenericDictionary<StatNames, float> {
    public Stats() {
        foreach (StatNames stat in Enum.GetValues(typeof(StatNames))) {
            Add(stat, 0);
        }
    }
}
