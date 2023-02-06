using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    public static float sizeModifier = 1;
    public static bool canTriggerSeeds;
    public float radius = 1f;


    void Start()
    {

        transform.localScale = new Vector3(sizeModifier, sizeModifier, 1);
        radius *= sizeModifier;
        UtilityFunctions.CameraShake();
        Destroy(gameObject, .7f);


        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, LayerMask.GetMask("Enemy"));

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<EnemyHealth>().GetHurt(6);
        }

        if (canTriggerSeeds)
        {

            Collider2D[] seedColliders = Physics2D.OverlapCircleAll(transform.position, radius, LayerMask.GetMask("Seed"));

            for (int i = 0; i < seedColliders.Length; i++)
            {
                if (canTriggerSeeds)
                {
                    seedColliders[i].GetComponent<Seed>().Activate();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
