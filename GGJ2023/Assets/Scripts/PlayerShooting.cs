using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{


    public GameObject seedPrefab;
    public GameObject seedActivatorPrefab;

    public float seedSpeed;
    public float activatorSpeed;
    public int shootCount;

    public Vector2 shootDirection;

    public float shootDelay;
    public float longShootDelay;
    float shootTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetShootDirection();

        if(Input.GetMouseButton(0)){
            if(shootTimer <= 0){
                if(shootCount%2 == 0){
                    ShootSeed();
                    shootTimer = shootDelay;
                }
                else{
                    ShootActivator();
                    shootTimer = longShootDelay;
                }
              
                shootCount++;
            }
        }
        shootTimer -= Time.deltaTime;
    }


    void SetShootDirection(){
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        shootDirection = mousePosition - (Vector2)transform.position;
        shootDirection = shootDirection.normalized;
    }
    void ShootSeed(){
       
            GameObject seed = Instantiate(seedPrefab, transform.position, Quaternion.identity);
            seed.GetComponent<Rigidbody2D>().velocity = shootDirection * seedSpeed;
        
    }

    void ShootActivator(){
        GameObject activator = Instantiate(seedActivatorPrefab, transform.position, Quaternion.identity);
        activator.GetComponent<Rigidbody2D>().velocity = shootDirection * activatorSpeed;

        activator.transform.rotation = Quaternion.Euler(0, 0, 90+UtilityFunctions.GetAngleFromDirection(shootDirection));
    }
}
