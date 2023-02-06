using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{


    public bool canPierce;
    public int damage;



    private void OnTriggerEnter2D(Collider2D other) {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null){
            enemyHealth.GetHurt(damage);
            if (!canPierce){
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null){
            enemyHealth.GetHurt(damage);
            if (!canPierce){
                Destroy(gameObject);
            }
        }
    }
}
