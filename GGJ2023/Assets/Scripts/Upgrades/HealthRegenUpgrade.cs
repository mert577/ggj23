using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthRegenUpgrade", menuName = "GGJ2023/HealthRegenUpgrade", order = 0)]
public class HealthRegenUpgrade : Upgrade
{
    public float healthRegenModifier;
    public override void ApplyUpgrade(){
        base.ApplyUpgrade();
        Tree.instance.healthRegen += healthRegenModifier;
    }
}
