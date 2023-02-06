using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "PeriodicBlastUpgrade", menuName = "GGJ2023/PeriodicBlastUpgrade", order = 0)]
public class PeriodicBlastUpgrade : Upgrade
{
    


   
    public override void ApplyUpgrade(){
        base.ApplyUpgrade();

        if(Tree.instance.periodicBlastActive == false){
            Tree.instance.periodicBlastActive = true;
            upgradeDescription = "Fire periodic blasts faster";
        }
        else{
            Tree.instance.periodicBlastCooldown *= .75f;
        }
        

        

    }
}
