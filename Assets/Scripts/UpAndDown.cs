using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDown : MonoBehaviour
{

    public Vector3 currentPos;
    public Vector3 delta;
    public Rigidbody rb;
    public Collider floor;
    public Collider ceiling;
    public float speed;
   
    void Start()
    {
        rb.velocity = new Vector3(0, (-speed/2) * Time.deltaTime, 0);
    }


    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.collider == floor) 
        {
            rb.velocity = new Vector3(0, speed * Time.deltaTime, 0);
        }

        if (collision.collider == ceiling)
        {
            rb.velocity = new Vector3(0, -speed * Time.deltaTime, 0);
        }

    }
}
