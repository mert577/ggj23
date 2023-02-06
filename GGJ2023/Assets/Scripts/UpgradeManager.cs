using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
public class UpgradeManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static UpgradeManager instance;
    
    public int materialCount;

    public bool choosingUpgrades;


    public List<Upgrade> upgradesAvaliable;

    public List<Upgrade> upgradesChosenThisRound;

    public Upgrade healthUpgrade; 


    public GameObject upgradeMenu;


    public UpgradeHolder upgradeHolderPrefab;


    public TextMeshProUGUI materialCountText;
     public TextMeshProUGUI materialCountText2;
    private void Awake() {
        if(instance != null){
            Destroy(gameObject);
        }else{
            instance = this;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        materialCountText.text = materialCount.ToString();
        materialCountText2.text = materialCount.ToString();

        if(Input.GetKeyDown(KeyCode.U)){
            OpenUpgradeMenu();
        }
    }


    void PopulateUpgradeMenu(int amountOfUpgrades){

        UpgradeHolder firstUpgrade = Instantiate(upgradeHolderPrefab, upgradeMenu.transform);
        firstUpgrade.Initiate(healthUpgrade, upgradeMenu.transform.GetChild(0));
        upgradesChosenThisRound.Clear();
        for(int i = 1; i < amountOfUpgrades; i++){
            if(upgradesAvaliable.Count == 0){
                break;
            }
            UpgradeHolder upgradeHolder = Instantiate(upgradeHolderPrefab, upgradeMenu.transform);
            upgradeHolder.Initiate(GetRandomUpgrade(), upgradeMenu.transform.GetChild(0));
        }

    }

    //choose an upgrade from the list of upgrades avaliable do not choose the same type of upgrade twice
    public Upgrade GetRandomUpgrade(){


        List<Upgrade> copyOfUpgradesAvaliable = new List<Upgrade>(upgradesAvaliable);
        int randomIndex = Random.Range(0, copyOfUpgradesAvaliable.Count);
        Upgrade temp = copyOfUpgradesAvaliable[randomIndex];
        int counter = 0;
        while(upgradesChosenThisRound.Contains(temp)&& counter<=50){
            randomIndex = Random.Range(0, copyOfUpgradesAvaliable.Count);
            temp = copyOfUpgradesAvaliable[randomIndex];
            counter++;
        }
        
        upgradesChosenThisRound.Add(temp);
        copyOfUpgradesAvaliable.RemoveAt(randomIndex);
        return temp;
    }
    public void OpenUpgradeMenu(){
      upgradeMenu.SetActive(true);

      upgradeMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -700, 0);
      upgradeMenu.GetComponent<RectTransform>().DOAnchorPosY(0, 0.5f).SetEase(Ease.OutBack);

      choosingUpgrades = true;
      EnemyMovement.canMove = false;
      PopulateUpgradeMenu(4);
      
    }



    public void OnUpgradeChosen(){


        //delete all childs of the upgrade menu
        foreach(Transform child in upgradeMenu.transform.GetChild(0)){
            Destroy(child.gameObject);
        }


        EnemyMovement.canMove = true;
        choosingUpgrades = false;
        upgradeMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -700, 0);
        upgradeMenu.SetActive(false);
        WaveSpawner.instance.StartNextStage();
    }
}
