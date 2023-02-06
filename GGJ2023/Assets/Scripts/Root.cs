using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{


    public Tree tree;

    public bool isBeingDrained;


    public bool canShoot;

    public float shootRadius;

    public GameObject nozzle;


    public float shootCooldown;

    public float shootTimer;
    public GameObject bulletPrefab;
    Transform target;


    public GameObject drainIcon;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckForDrainer());
    }

    // Update is called once per frame
    void Update()
    {
        if(target==null){
            FindClosestEnemyInRadius();
        }
        else{
            if(Vector2.Distance(transform.position, target.position) > shootRadius){
                target = null;
            }
        }

        if(target!=null){
            if(canShoot){
               if(shootTimer <= 0){
                    Shoot();
                    shootTimer = shootCooldown;
                }
                else{
                   
                }
                
            }
        }

         shootTimer -= Time.deltaTime;


         if(canShoot && target!=null){
           
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.forward, target.position - transform.position), Time.deltaTime * 5f);
            nozzle.gameObject.SetActive(true);        
         }
         else{
            nozzle.gameObject.SetActive(false);
         }
    }





    void Shoot(){
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector2 direction = target.position - transform.position;
        bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * 15f;
        Destroy(bullet, 2f);

    }
     void FindClosestEnemyInRadius(){
        Collider2D[] closestEnemy = Physics2D.OverlapCircleAll(transform.position, shootRadius, LayerMask.GetMask("Enemy"));
       
        float closestDistance = Mathf.Infinity;
        foreach(Collider2D enemy in closestEnemy){
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if(distance < closestDistance){
                closestDistance = distance;
                target = enemy.transform;
            }
        }


    }



    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Drainer"){
            isBeingDrained = true;
            tree.CalculateDrainAmount();
          
          
          
        }
    }

    IEnumerator CheckForDrainer(){
        yield return new WaitForSeconds(0.5f);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f, LayerMask.GetMask("Drainer"));
        if(colliders.Length > 0){
            isBeingDrained = true;
            tree.CalculateDrainAmount();
            
            
        }
        else{
            isBeingDrained = false;
            tree.CalculateDrainAmount();
        }

        StartCoroutine(CheckForDrainer());
    }

     private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Drainer"){
            isBeingDrained = false;
            tree.CalculateDrainAmount();
        }
        
    }
}
