using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Vector3 = UnityEngine.Vector3;

public class treeFalling : MonoBehaviour
{
   
    public Vector3 currentRotation;
    public Vector3 angleToRotate;
    public float voiceAngle;
    public AudioClip fallSound;
    public AudioSource SourceVoice;
    private bool voiceOn = false;

    


    public void Start()
    {
        currentRotation = new Vector3(currentRotation.x % 360f, currentRotation.y % 360f, currentRotation.z % 360f);
        angleToRotate = new Vector3(angleToRotate.x % 360f, angleToRotate.y % 360f, angleToRotate.z % 360f);
        this.transform.eulerAngles = currentRotation;
        
    }

    private void Update()
    {
        if (currentRotation.x % 360f < 90)
        {
            currentRotation = currentRotation + angleToRotate * Time.deltaTime;
            currentRotation = new Vector3(currentRotation.x % 360f, currentRotation.y % 360f, currentRotation.z % 360f);
            this.transform.eulerAngles = currentRotation;

            if (currentRotation.x % 360f >= voiceAngle && !voiceOn) 
            {
  
                SourceVoice.clip = fallSound;
                SourceVoice.Play();
                voiceOn = true;
         
            }

            
        }
      
    }

}
