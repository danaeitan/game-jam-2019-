using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public int pourThreshold = 45;
    public Transform origin = null;

    public GameObject streamPrefab = null;
    private bool isPouring = false;
    private OilStream currentStream = null;

    private void Update()
    {
        bool pourCheck = CalculatePourAngel() < pourThreshold;
        if (isPouring != pourCheck)
        {
            isPouring = pourCheck;
        }

        if (isPouring)
        {
            StartPour();
        }

        else
        {
            EndPour();
        }
    }

    private void StartPour()
    {
        print("Start");
        currentStream = CreateStream();
        currentStream.Begin();

    }

    private void EndPour()
    {
        print("End");
    }

    private float CalculatePourAngel()
    {
        return transform.forward.y * Mathf.Rad2Deg;
    }

    private OilStream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        return streamObject.GetComponent<OilStream>();
    }








}
