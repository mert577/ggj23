using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public GameObject target;

    public float speed;

    public List<float> interestMap;
    public List<float> dangerMap;

    public float dangerRadius = 5f;
   
    public List<Transform> obstacles;

    public List<Transform> otherEnemies;

    public static bool canMove= true;


    
    Rigidbody2D rb;
  GameObject[] targets;
    float randomSpeedModifier;
    List<Vector2> EightDirections = new List<Vector2>(){
        new Vector2(0,1),
        new Vector2(1,1),
        new Vector2(1,0),
        new Vector2(1,-1),
        new Vector2(0,-1),
        new Vector2(-1,-1),
        new Vector2(-1,0),
        new Vector2(-1,1)
    };


    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        randomSpeedModifier = Random.Range(0.8f, 1.2f);

    }

    // Start is called before the first frame update
    void Start()
    {  targets = GameObject.FindGameObjectsWithTag("Root");
        StartCoroutine(UpdateObstacles());
        StartCoroutine(FindClosestTarget());
      
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate(){
        GetObstacleMap();
        GetInterestMap();
        
        if(canMove){
           rb.velocity = GetDirection()*speed*randomSpeedModifier;
        }
        else{
            rb.velocity = Vector2.zero;
        }
       
    }



    
    IEnumerator UpdateObstacles()
    {


        obstacles.Clear();
        otherEnemies.Clear();


        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, dangerRadius, LayerMask.GetMask("Enemy"));
        foreach (Collider2D collider in colliders)
        {

            otherEnemies.Add(collider.transform);

        }


        Collider2D[] colliders2 = Physics2D.OverlapCircleAll(transform.position, dangerRadius, LayerMask.GetMask("Obstacle"));
        foreach (Collider2D collider in colliders)
        {

            obstacles.Add(collider.transform);

        }

        yield return new WaitForSeconds(.5f);
        StartCoroutine(UpdateObstacles());

    }


    IEnumerator FindClosestTarget()
    {
       
        float closestDistance = Mathf.Infinity;
        foreach (GameObject possibleTarget in targets)
        {
            float distance = Vector2.Distance(transform.position, possibleTarget.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = possibleTarget;
            }
        }

        yield return new WaitForSeconds(.5f);
        StartCoroutine(FindClosestTarget());
    }




    void GetObstacleMap()
    {


        //init danger map with 8 zeroes
        dangerMap = new List<float>() { 0, 0, 0, 0, 0, 0, 0, 0 };

    //remove all destoryed elements
        obstacles.RemoveAll(item => item == null);
        otherEnemies.RemoveAll(item => item == null);
        foreach (Transform obstacle in obstacles)
        {
                  if(!obstacle){
                continue;
            }
        
            Vector2 direction = (Vector2)obstacle.position - (Vector2)transform.position;

            for (int i = 0; i < EightDirections.Count; i++)
            {
                float dot = Vector2.Dot(direction.normalized, EightDirections[i]);
                dangerMap[i] += dot *(1f - (direction.magnitude / dangerRadius));


            }

        }

         foreach (Transform obstacle in otherEnemies)
        {

                if(!obstacle){
                continue;
            }
            Vector2 direction = (Vector2)obstacle.position - (Vector2)transform.position;

            for (int i = 0; i < EightDirections.Count; i++)
            {
                float dot = Vector2.Dot(direction.normalized, EightDirections[i]);

                dot = 1 -  Mathf.Abs(dot-.65f);
                dangerMap[i] += dot*.3f *(1f - (direction.magnitude / dangerRadius));


            }

        }
    }


    void GetInterestMap()
    {
        interestMap = new List<float>() { 0, 0, 0, 0, 0, 0, 0, 0 };
        Vector2 direction = (Vector2)target.transform.position - (Vector2)transform.position;
        for (int i = 0; i < EightDirections.Count; i++)
        {
            float dot = Vector2.Dot(direction.normalized, EightDirections[i]);
            interestMap[i] = dot;
        }
    }


    Vector2 GetDirection(){
        List<float> directionMap = new List<float>();
        for (int i = 0; i < EightDirections.Count; i++)
        {
            directionMap.Add(interestMap[i] - dangerMap[i]);
        }

        //get the average of the eight directions multiplied by the direction map
        Vector2 direction = Vector2.zero;
        for (int i = 0; i < EightDirections.Count; i++)
        {
            direction += EightDirections[i] * directionMap[i];
        }

        return direction.normalized;

    }
    

}