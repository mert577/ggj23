using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static UpgradeManager instance;
    
    public int materialCount;


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
        
    }
}
