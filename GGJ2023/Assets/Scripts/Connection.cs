using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : MonoBehaviour
{

    public Transform tree;
    public Transform player;
    [SerializeField]
    LineRenderer lineRenderer;

    public int numberOfVertices = 5;


    public float perlinNoiseScale;
    public float perlinNoiseScaleY;

    public float magnitude;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetVertexPositions();
    }



    void SetVertexPositions(){
        lineRenderer.positionCount = numberOfVertices;
        Vector3[] positions = new Vector3[numberOfVertices];
        for(int i = 0; i < numberOfVertices; i++){
            positions[i] = Vector3.Lerp(tree.position, player.position, (float)i / (numberOfVertices - 1) );
            positions[i] +=    (Vector3)UtilityFunctions.GetOrthagonalDirection((Vector2)tree.position - (Vector2) player.position).normalized *  UtilityFunctions.Map(Mathf.PerlinNoise(i*perlinNoiseScale,Time.time*perlinNoiseScaleY),0,1f,-magnitude,magnitude);
        }
        lineRenderer.SetPositions(positions);
    }
}
