using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComboSeedUpgrade", menuName = "GGJ2023/ComboSeedUpgrade", order = 0)]
public class ComboSeedUpgrade : Upgrade
{
    
    public override void ApplyUpgrade(){
        base.ApplyUpgrade();
        Explosion.canTriggerSeeds = true;
        Seed.stayOnGroundTime = 5f;
    }
}
