using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform playersInitialPosition; //Place a gameObject indicating the initialPosition;
    public GameObject player; //this is the OVRCameraRig 
    public Canvas mainCanvas;

    public GameObject fire;
    public Vector3 STARTING_POS = new Vector3(0.02f, 0.02f, 2f);
    public Vector3 STARTING_SCALE = new Vector3(1, 1, 1);
    public float FIRE_DURATION;
    private Camera mainCamera; //this is the actual camera with the fade option

    private void Start()
    {
        Instantiate(fire, STARTING_POS, Quaternion.identity);
        fire.transform.localScale = STARTING_SCALE;
        fire.GetComponent<FlameCounter>().LEVEL_TIME = FIRE_DURATION;
        mainCamera = Camera.main;
        mainCanvas.gameObject.SetActive(false);
    }
    //This is called when: 1. The fire has ceased, 2.The player dropped the oil 3.when the player collided with obsticle 
    public void OnEndGame()
    {
        mainCanvas.gameObject.SetActive(true);
    }

    //this method is called by button press. 
    public void OnRestartGame()
    {
        StartCoroutine(OnRestartRoutine());
    }

    //play the fade in / out method 
    private IEnumerator OnRestartRoutine()
    {
        var fader = mainCamera.GetComponent<OVRScreenFade>();
        fader.FadeOut();
        yield return new WaitForSeconds(2f); //the time it takes to fade 
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void OnWinGame()
    {
        mainCanvas.gameObject.SetActive(true);
    }
}
