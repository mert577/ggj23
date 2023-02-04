using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{


    public Tree tree;

    public bool isBeingDrained;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Drainer"){
            isBeingDrained = true;
            tree.CalculateDrainAmount();
          
        }
    }

     private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Drainer"){
            isBeingDrained = false;
            tree.CalculateDrainAmount();
        }
        
    }
}
