using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MackySoft.Choice;

public class WaveSpawner : MonoBehaviour
{

    public float spawnRadius;


    public List<WeightedEnemy> weightedEnemies;


    public float waveTime;

    [SerializeField]
    float waveTimer;


    public int numberOfWaves;

    public TextMeshProUGUI waveText;
    public int numberOfStages;


    public static WaveSpawner instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
      
    }



    public void StartSpawning(){
          StartCoroutine(SpawnWave());
    }
    // Update is called once per frame
    void Update()
    {
        if(!UpgradeManager.instance.choosingUpgrades && !Tree.instance.gameOverScreen.activeInHierarchy && Tree.instance.gameStarted){
          
          waveTimer += Time.deltaTime;
        }
       
        if (waveTimer >= waveTime)
        {
            waveTimer = 0;
            NextStage();
        }
        //write wave timer to UI as an int
        waveText.text = Mathf.RoundToInt(waveTime - waveTimer).ToString();
        SetWeights();
    }



    public void SetWeights()
    {
        if (numberOfStages < 2)
        {
            weightedEnemies[0].weight = 1;
            weightedEnemies[1].weight = 0;
            weightedEnemies[2].weight = 0;
        }
        else if (numberOfStages < 3)
        {
            weightedEnemies[0].weight = 1;
            weightedEnemies[1].weight = .4f;
            weightedEnemies[2].weight = .1f;
        }
        else if (numberOfStages < 4)
        {
            weightedEnemies[0].weight = 1.4f;
            weightedEnemies[1].weight = 1;
            weightedEnemies[2].weight = .3f;
        }
        else if (numberOfStages < 5)
        {
              weightedEnemies[0].weight = 2f;
            weightedEnemies[1].weight = 2f;
            weightedEnemies[2].weight = 1.5f;
        }
        else
        {
             weightedEnemies[0].weight = 1f;
            weightedEnemies[1].weight = 2f;
            weightedEnemies[2].weight = 2f;
        }
    }

    public void NextStage()
    {
        StopCoroutine(SpawnWave());
        numberOfStages++;
        UpgradeManager.instance.OpenUpgradeMenu();
    }


    public void StartNextStage()
    {
        StartCoroutine(SpawnWave());
    }

    Vector2 GetWaveSpawnInterval()
    {
        if (numberOfWaves < 25)
        {
            return new Vector2(8, 12);
        }
        else if (numberOfWaves < 44)
        {
            return new Vector2(7, 11);
        }
        else if (numberOfWaves < 68)
        {
            return new Vector2(6, 10);
        }
        else if (numberOfWaves < 80)
        {
            return new Vector2(4, 7);
        }
        else
        {
            return new Vector2(3.5f, 7);
        }
    }
    IEnumerator SpawnWave()
    {

        while (waveTimer < waveTime && !UpgradeManager.instance.choosingUpgrades)
        {
            SpawnEnemyGroups(Random.Range(2, 4));
            numberOfWaves++;

            yield return new WaitForSeconds(Random.Range(GetWaveSpawnInterval().x, GetWaveSpawnInterval().y));

        }

    }

    void SpawnEnemyGroups(int numberOfGroups)
    {
        for (int i = 0; i < numberOfGroups; i++)
        {   
            GameObject g = ChooseWeightedEnemy().enemyType;
            SpawnEnemyGroup(g);
        }
    }

    void SpawnEnemyGroup(GameObject g)
    {
        GameObject enemyGroup = Instantiate(g, ChooseRandomPointOnCircle(spawnRadius), Quaternion.identity);

    }

    Vector2 ChooseRandomPointOnCircle(float radius)
    {

        return Random.insideUnitCircle.normalized * radius;
    }






    WeightedEnemy ChooseWeightedEnemy(){

        var weightedSelector = weightedEnemies.ToWeightedSelector(x => x.weight);

        return weightedSelector.SelectItemWithUnityRandom();
    }


    [System.Serializable]
    public class WeightedEnemy
    {
        public GameObject enemyType;
        public float weight;
    }




}
