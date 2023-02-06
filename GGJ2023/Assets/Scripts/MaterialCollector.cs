using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MaterialCollector : MonoBehaviour
{


    public float collectTime;
    public AudioSource collectSound;
    public AudioClip collectClip;
    public AnimationCurve collectCurve;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Material"){
           StartCoroutine(LerpMaterialToPosition(other.gameObject));
        }
    }


    IEnumerator LerpMaterialToPosition(GameObject material){
        Vector3 startPosition = material.transform.position;
        material.GetComponent<Collider2D>().enabled = false;
        float time = collectTime;
        while(time >= 0f){
            time -= Time.deltaTime;
            material.transform.position = myLerp(startPosition, transform.position, collectCurve.Evaluate(1f-  (time/collectTime)));
            yield return null;
        }
        yield return new WaitForEndOfFrame();
        UpgradeManager.instance.materialCount++;
        collectSound.PlayOneShot(collectClip);
        Destroy(material);
    }
Vector3 myLerp(Vector3 start, Vector3 end, float t)
{
    return start + (end - start) * t;
}
}



