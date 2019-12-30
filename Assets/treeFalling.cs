using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class treeFalling : MonoBehaviour
{
    public Transform tree;
    public Rigidbody rb;
    public float speed;
    public Vector3 currentRotation;
    public Vector3 angleToRotate;


    public void Start()
    {
        currentRotation = new Vector3(currentRotation.x % 360f, currentRotation.y % 360f, currentRotation.z % 360f);
        angleToRotate = new Vector3(angleToRotate.x % 360f, angleToRotate.y % 360f, angleToRotate.z % 360f);
        this.transform.eulerAngles = currentRotation;
        
    }

    private void FixedUpdate()
    {
        if (currentRotation.x % 360f < 90)
        {
            currentRotation = currentRotation + angleToRotate * Time.deltaTime;
            currentRotation = new Vector3(currentRotation.x % 360f, currentRotation.y % 360f, currentRotation.z % 360f);
            this.transform.eulerAngles = currentRotation;
        }
      
    }

}
