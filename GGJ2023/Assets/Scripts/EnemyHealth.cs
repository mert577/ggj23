using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{


    public int maxHealth =1;
    public int currentHealth;


    public GameObject hurtParticlePrefab;
    public GameObject deathParticlePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Damager"){
            GetHurt(1);
        
        }
    }

    public void GetHurt(int damage){
        currentHealth -= damage;
        GameObject hurtParticle = Instantiate(hurtParticlePrefab, transform.position, Quaternion.identity);
        Destroy(hurtParticle, 1f);
        if(currentHealth <= 0){
            
            Die();
        }
    }


    public void Die(){
        GameObject deathParticle = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        Destroy(deathParticle, 1f);
        Destroy(gameObject);
    }
}
