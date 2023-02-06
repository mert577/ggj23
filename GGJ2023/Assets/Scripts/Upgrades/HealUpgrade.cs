using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealUpgrade", menuName = "GGJ2023/HealUpgrade", order = 0)]

public class HealUpgrade : Upgrade
{
    public float healAmount;
    public override void ApplyUpgrade(){
        base.ApplyUpgrade();
        Tree.instance.Heal(healAmount);
    }
}
