using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MainMenuSequence : MonoBehaviour
{

    public RectTransform titleText;
    public RectTransform startButton;

    public GameObject ingameUI;
    // Start is called before the first frame update
    void Start()
    {
     //   Screen.SetResolution(800, 800,true);

        titleText.anchoredPosition = new Vector2(0, 150);
          startButton.anchoredPosition = new Vector2(0, -121);
        titleText.DOAnchorPosY(-137, 1.5f).SetEase(Ease.OutQuad).OnComplete(() => {
          
            startButton.DOAnchorPosY(101, 1.5f).SetEase(Ease.OutQuad);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartTheGame(){
        
        Tree.instance.gameStarted = true;
        WaveSpawner.instance.StartSpawning();
        ingameUI.SetActive(true);
        gameObject.SetActive(false);

    }
}
