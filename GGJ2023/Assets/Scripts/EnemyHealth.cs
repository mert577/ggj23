using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{


    public int maxHealth =1;
    public int currentHealth;


    public GameObject hurtParticlePrefab;
    public GameObject deathParticlePrefab;

    public GameObject materialPrefab;

    public float knockbackForce = 3f;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void GetHurt(int damage){
        currentHealth -= damage;
        GameObject hurtParticle = Instantiate(hurtParticlePrefab, transform.position, Quaternion.identity);
        Destroy(hurtParticle, 1f);
        if(currentHealth <= 0){
            
            Die();
        }
    }


    void SpawnMaterials(int amount){
        for(int i = 0; i < amount; i++){
            GameObject material = Instantiate(materialPrefab, transform.position + Random.onUnitSphere*.1f, Quaternion.Euler(0,0,Random.Range(0,360)));
            //apply force to material away from enemy
            Rigidbody2D rb = material.GetComponent<Rigidbody2D>();
            rb.AddForce((material.transform.position - transform.position).normalized *knockbackForce, ForceMode2D.Impulse);
        }
    }

    public void Die(){
        GameObject deathParticle = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        SpawnMaterials(Random.Range(1,4));
        Destroy(deathParticle, 1f);
        Destroy(gameObject);
    }
}
