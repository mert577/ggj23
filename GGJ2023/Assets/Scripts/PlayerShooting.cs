using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{


    public GameObject seedPrefab;
    public GameObject seedActivatorPrefab;

    public float seedSpeed;
    public float activatorSpeed;
    public int shootCount;

    public Vector2 shootDirection;

    public Image chargeBar;
    public float chargeAmount;
    public float shootDelay;
    public float longShootDelay;

    public float chargeTime;
    float shootTimer;

    bool isChargeActive;

    public float seedShootTimer;


    public float percent;

    public static PlayerShooting instance;

    void Awake(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
           chargeBar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        SetShootDirection();

        if(Input.GetMouseButton(0)){
            if(shootTimer <= 0){
                
              
                    ShootActivator();
                    shootTimer = shootDelay;
                
              
                shootCount++;
            }
        }

        if(seedShootTimer <= 0){
      

        if(Input.GetMouseButtonDown(1)){
            StartCharging();
        }


        if(Input.GetMouseButtonUp(1)){
            StopCharging();
        }
        

        if(isChargeActive){
            chargeAmount += Time.deltaTime;
             percent = chargeAmount / chargeTime;
             percent = Mathf.Clamp(percent, 0, 1);
             chargeBar.fillAmount = percent;
          //   chargeBar.transform.position = transform.position;
        }
        else{
            chargeAmount = 0;
        }

             
        }
        if(!isChargeActive){
            chargeBar.gameObject.SetActive(false);
        }
        else{
            chargeBar.gameObject.SetActive(true);
        }
        shootTimer -= Time.deltaTime;
        seedShootTimer -= Time.deltaTime;
    }


    void StartCharging(){
        isChargeActive = true;
        chargeBar.gameObject.SetActive(true);
    }

    void StopCharging(){
        isChargeActive = false;
         // chargeBar.gameObject.SetActive(false);
          seedShootTimer = longShootDelay;
        ShootSeed(percent);


    }


    void SetShootDirection(){
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        shootDirection = mousePosition - (Vector2)transform.position;
        shootDirection = shootDirection.normalized;
    }
    void ShootSeed(float power){
       
            GameObject seed = Instantiate(seedPrefab, transform.position, Quaternion.identity);
            seed.GetComponent<Rigidbody2D>().velocity = shootDirection * seedSpeed*power;
        
    }

    void ShootActivator(){
        GameObject activator = Instantiate(seedActivatorPrefab, transform.position, Quaternion.identity);
        activator.GetComponent<Rigidbody2D>().velocity = shootDirection * activatorSpeed;

        activator.transform.rotation = Quaternion.Euler(0, 0, 90+UtilityFunctions.GetAngleFromDirection(shootDirection));
    }
}
