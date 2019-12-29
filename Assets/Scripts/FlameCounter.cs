using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FlameCounter : MonoBehaviour
{
    public const float LEVEL_TIME = 120; //in seconds. 
    private float counter;
    private Vector3 startingValues; 
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        sta
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        transform.localScale -= new Vector3(1, 1, 1) * (1f / LEVEL_TIME) * Time.deltaTime;
        print(Time.deltaTime);
        if (counter >= LEVEL_TIME)
        {

        }


        
    }


}
