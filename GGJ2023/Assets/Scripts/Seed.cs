using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Seed : MonoBehaviour
{

    public GameObject explodePrefab;

    public static float stayOnGroundTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestorySelf());
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Activator"){
            
            Activate();
            Destroy(other.gameObject);
        }
    }

    IEnumerator DestorySelf(){  
        yield return new WaitForSeconds(stayOnGroundTime);
        Tween tween = transform.DOScale(0f, 0.3f);
        yield return tween.WaitForCompletion();

        Destroy(gameObject);
    }
   public  void Activate(){

        //TODO ACTUAL ACTIVATION CODE
        GameObject explode = Instantiate(explodePrefab, transform.position, Quaternion.identity);
     //  UtilityFunctions.TimeStop(0.25f);

        Destroy(gameObject);
    }
}
