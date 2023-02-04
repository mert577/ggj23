using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerMovement : MonoBehaviour
{

    public float dashTime;
    public bool isDashing;
    public float dashForce;

    public float dashDrag;
    public Rigidbody2D rb;
    public float speed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }




    private void FixedUpdate()
    {
        if (!isDashing)
        {
            Move();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dash();
        }
    }



    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, y);
        direction = Vector2.ClampMagnitude(direction, 1f);

        rb.velocity = (direction * speed);
    }


    void Dash()
    {


        StartCoroutine(__());
        IEnumerator __()
        {


            isDashing = true;

            float drag = rb.drag;

            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            Vector2 direction = new Vector2(x, y);
            direction = Vector2.ClampMagnitude(direction, 1f);
            


            rb.velocity = dashForce * direction;
            Debug.Log(rb.velocity);
            
            rb.drag = dashDrag;
            float timer = dashTime;
           
            while (timer > 0)
            {
                float t = timer / dashTime;







                timer -= Time.deltaTime;
                rb.drag -= (dashDrag - drag) / (dashTime / Time.deltaTime);
                yield return null;
            }
           
            //rb.velocity = Vector2.zero;
            rb.drag = drag;
            isDashing = false;
       
            yield return null;
        }
    }
}
