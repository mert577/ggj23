using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FasterShootingUpgrade", menuName = "GGJ2023/FasterShootingUpgrade", order = 0)]
public class FasterShootingUpgrade : Upgrade
{
    
    public override void ApplyUpgrade(){
        base.ApplyUpgrade();
        PlayerShooting.instance.shootDelay *= 0.75f;
        PlayerShooting.instance.longShootDelay *= 0.75f;
    }
}
