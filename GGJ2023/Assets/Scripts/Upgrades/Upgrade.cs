using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Upgrade", menuName = "GGJ2023/Upgrade", order = 0)]


public class Upgrade : ScriptableObject
{
    public string upgradeName;
    public string upgradeDescription;

    public int upgradeCost;


    public virtual void ApplyUpgrade(){
        UpgradeManager.instance.upgradesAvaliable.Remove(this);
        Debug.Log("Upgrade Applied");
    }
}
