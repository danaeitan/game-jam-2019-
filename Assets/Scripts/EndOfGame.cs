using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfGame : MonoBehaviour
{
    public GameManager gm;
    private void OnTriggerEnter(Collider other)
    {
        print("you won!");
        gm.OnWinLevel();


    }
   
}

