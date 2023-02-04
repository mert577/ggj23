using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    public GameObject enemyGroupPrefab;

    public float spawnRadius;


    public int numberOfWaves;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    // Update is called once per frame
    void Update()
    {
        
    }





    Vector2 GetWaveSpawnInterval(){
        if(numberOfWaves < 10){
            return new Vector2(7,10);
        }
        else if(numberOfWaves < 22){
            return new Vector2(5,8);
        }
        else if(numberOfWaves < 28){
            return new Vector2(4,7);
        }
        else if(numberOfWaves < 35){
            return new Vector2(4,6);
        }
        else{
            return new Vector2(3,4);
        }
    }
    IEnumerator SpawnWave(){

        while(true){
             SpawnEnemyGroups(Random.Range(2,4  ));
            numberOfWaves++;
              yield return new WaitForSeconds(Random.Range(GetWaveSpawnInterval().x, GetWaveSpawnInterval().y));
         
        }
       
    }

    void SpawnEnemyGroups(int numberOfGroups){
        for(int i = 0; i < numberOfGroups; i++){
            SpawnEnemyGroup();
        }
    }

    void SpawnEnemyGroup(){
        GameObject enemyGroup = Instantiate(enemyGroupPrefab, ChooseRandomPointOnCircle(spawnRadius), Quaternion.identity);
      
    }

    Vector2 ChooseRandomPointOnCircle(float radius){
       
        return  Random.insideUnitCircle.normalized  *radius;
    }


}
