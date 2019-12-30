using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform playersInitialPositions; //Place a gameObject indicating the initialPosition;
    public GameObject player; //this is the OVRCameraRig 
    public Canvas mainCanvas;
    public UnityEvent [] levels;

    public GameObject fire;
    public Vector3 STARTING_POS;
    public Vector3 STARTING_SCALE;
    public float FIRE_DURATION;
    private Camera mainCamera; //this is the actual camera with the fade option
    private EnemyManager enemyManager;
    private PlayerManager playerManager;
    private int levelCount;

    private void Start()
    {
        mainCamera = Camera.main;
        
        //mainCanvas.gameObject.SetActive(false);
        CreateFire();
    }

    //This is called when: 1. The fire has ceased, 2.The player dropped the oil 3.when the player collided with obsticle
    public void OnRestartLevel()
    {
       mainCanvas.gameObject.SetActive(true);
        
       StartCoroutine(OnRestartLevelRoutine());
    }

    //play the fade in / out method 
    private IEnumerator OnRestartLevelRoutine()
    {
        int currentLevel = levelCount;
        var fader = mainCamera.GetComponent<OVRScreenFade>();
        fader.FadeOut();
        yield return new WaitForSeconds(2f); //the time it takes to fade 
        levels[currentLevel].Invoke();
        mainCamera.transform.position = playersInitialPositions.position;
        fader.FadeIn();
    }

    //this is called when player succeeds
    public void OnWinLevel()
    {
        levelCount++;
        StartCoroutine(LoadNextLevel(levelCount));
    }

    private IEnumerator LoadNextLevel(int levelCount)
    {
        var fader = mainCamera.GetComponent<OVRScreenFade>();
        fader.FadeOut();
        yield return new WaitForSeconds(2f); //the time it takes to fade 
        levels[levelCount].Invoke();
        fader.FadeIn();

    }
    public void OnWinGame()
    {
        mainCanvas.gameObject.SetActive(true);
        StartCoroutine(OnRestartGameRoutine());
    }

    IEnumerator OnRestartGameRoutine()
    {
        var fader = mainCamera.GetComponent<OVRScreenFade>();
        fader.FadeOut();
        yield return new WaitForSeconds(4);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    private void CreateFire()
    {
        Instantiate(fire, STARTING_POS, Quaternion.identity);
        fire.transform.localScale = STARTING_SCALE;
        fire.GetComponent<FlameCounter>().fireDuration = FIRE_DURATION;
    }
}
