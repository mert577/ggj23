using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeHolder : MonoBehaviour
{
    public Upgrade upgrade;

    public TextMeshProUGUI upgradeNameText;
    public TextMeshProUGUI upgradeCostText;
    public TextMeshProUGUI upgradeDescriptionText;

    public void Initiate(Upgrade upgrade, Transform parent){
        transform.parent = parent;
        this.upgrade = upgrade;
        upgradeNameText.text = upgrade.upgradeName;
        upgradeCostText.text = "Cost: " + upgrade.upgradeCost.ToString();
        upgradeDescriptionText.text = upgrade.upgradeDescription;
    }
    public void ApplyUpgrade(){
        if(UpgradeManager.instance.materialCount < upgrade.upgradeCost){
            Debug.Log("Not enough materials");
            return;
        }
          
        UpgradeManager.instance.materialCount -= upgrade.upgradeCost;
        upgrade.ApplyUpgrade();
        UpgradeManager.instance.OnUpgradeChosen();
    }

}
