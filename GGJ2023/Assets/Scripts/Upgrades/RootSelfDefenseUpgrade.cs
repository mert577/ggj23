using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RootSelfDefenseUpgrade", menuName = "GGJ2023/RootSelfDefenseUpgrade", order = 0)]
public class RootSelfDefenseUpgrade : Upgrade
{

    public override void ApplyUpgrade(){

        base.ApplyUpgrade();
       foreach(Root r in Tree.instance.roots){
        if(!r.canShoot){
            r.canShoot = true;
            break;
        }
       }
    }
}
