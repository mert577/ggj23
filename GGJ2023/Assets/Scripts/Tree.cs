using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{


    public float MaxHealth;
    public float CurrentHealth;

    public float healthRegen;

    bool isBeingDrained;


    public float drainAmount;
    public List<Root> roots;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentHealth -= drainAmount * Time.deltaTime;
        if(drainAmount<=0){
            CurrentHealth += healthRegen * Time.deltaTime;
        }

        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
     
    }

    public void CalculateDrainAmount(){
        drainAmount = 0;
        foreach(Root root in roots){
            if(root.isBeingDrained){
                drainAmount += 0.25f;
            }
            
        }
    }   

}
