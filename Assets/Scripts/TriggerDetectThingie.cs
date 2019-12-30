using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TriggerDetectThingie : MonoBehaviour
{
    public List<Collider> touching;

    void Start () 
    {
        touching = new List<Collider>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        touching.Add(other);
    }
    void OnTriggerExit(Collider other)
    {
        touching.Remove(other);
    }
} 
