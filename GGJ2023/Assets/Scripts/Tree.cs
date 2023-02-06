using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Tree : MonoBehaviour
{


    public float surviveTime=0;
    public float MaxHealth;
    public float CurrentHealth;

    public float healthRegen;

    bool isBeingDrained;


    public float drainAmount;
    public List<Root> roots;

    public TextMeshProUGUI surviveText;
    public bool periodicBlastActive;


    public float periodicBlastCooldown = 3f;

    float periodicBlastTimer;

    public GameObject bulletPrefab;

    public float bulletSpeed;


    public static Tree instance;

    public GameObject gameOverScreen;
    
    public float rotateSpeed;

    public bool gameStarted;
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
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentHealth -= drainAmount * Time.deltaTime;
        if(drainAmount<=0){
            CurrentHealth += healthRegen * Time.deltaTime;
        }

        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        if(CurrentHealth<=0){
            GameOver();
        }


        periodicBlastTimer -= Time.deltaTime;

        if(periodicBlastActive && periodicBlastTimer <= 0){
            SpawnBulletsInEightDirections();
            periodicBlastTimer = periodicBlastCooldown;
        }


        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);

        if(!UpgradeManager.instance.choosingUpgrades&& gameStarted){
            surviveTime += Time.deltaTime;
        }
    }




    void SpawnBulletsInEightDirections(){
        for(int i = 0; i < 8; i++){
            float angle = i * (360f/8f);
            Vector2 direction = new Vector2(Mathf.Cos(angle* Mathf.Deg2Rad ), Mathf.Sin(angle*Mathf.Deg2Rad));
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction*bulletSpeed;
        }
    }


    public void CalculateDrainAmount(){
        drainAmount = 0;
        foreach(Root root in roots){
            if(root.isBeingDrained){
                drainAmount += 0.25f;
            }
            
        }
    }   


    public void Heal(float amount){
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
    }
    void GameOver(){
        Debug.Log("Game Over");
        gameOverScreen.SetActive(true);
        surviveText.text = "You survived for " + Mathf.RoundToInt (surviveTime).ToString() + " seconds";
    }


    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
