using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SeedExplosionSizeUpgrade", menuName = "GGJ2023/SeedExplosionSizeUpgrade", order = 0)]
public class SeedExplosionSizeUpgrade : Upgrade
{

    public float explosionSizeModifier;
    public override void ApplyUpgrade(){
        base.ApplyUpgrade();
        Explosion.sizeModifier *= explosionSizeModifier;
    }  
}
