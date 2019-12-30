using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class FlameCounter : MonoBehaviour
{
    public float LEVEL_TIME = 120f; //in seconds. 
    private float counter;
    private Vector3 startingValues;
    // Start is called before the first frame update
    public void Start()
    {
        counter = 0;
        startingValues = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        
        Vector3 newScale = transform.localScale - startingValues * (1f / LEVEL_TIME) * Time.deltaTime;
        transform.localScale = new Vector3(Math.Max(newScale.x, 0), Math.Max(newScale.y, 0), Math.Max(newScale.z, 0));
        if (counter >= LEVEL_TIME)
        {
            print("GAME OVER!");
        }
        
    }
}
