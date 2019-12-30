using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using OVR;
using OVR.OpenVR;

public class OilCup : MonoBehaviour
{
    [Header("Subcomponents")]
    [SerializeField]
    private GameObject oilCupMesh;
    [SerializeField]
    private GameObject oilFakeSurface;
    [SerializeField]
    private TriggerDetectThingie fakeSurfaceCollider;
    [SerializeField]
    private TriggerDetectThingie spillCollider;
    [SerializeField]
    private GameObject point;

    [Header("Oil spill")]
    [SerializeField]
    private OilDropletsSlpatOnCollision spillPrefab;
    [SerializeField]
    private float oilSurfaceReduceRate;
    [SerializeField]
    [Range(0.0f, 1.0f)] private float spillLoseThreshold;

    [Header("Shake oil surface caused by fast movement")]
    [SerializeField]
    private float movementShakeThreshold;
    [SerializeField]
    private float movementShakeMultiplier;
    [SerializeField]
    [Range(0.0f, 1.0f)] private float oilSurfaceStabilizeRate;

    [Header("Random cup rotation when hit obstacle")]
    [SerializeField]
    private bool rotateCupRandomly;
    [SerializeField]
    private float rotateCupRandomPower;

    [Header("Oculus")]
    [SerializeField]
    private OVRInput.Controller controller;
    
    [Header("Debug")]
    [SerializeField]
    private bool useMouseInsteadOfOculus = true;

    private Vector3 lastMouse;
    private float initialFakeSurfaceHeight;
    private float initialSpillColliderHeight;
    private bool isInstantiated;


    private void Start()
    {
        lastMouse = Input.mousePosition;
        initialFakeSurfaceHeight = oilFakeSurface.transform.localPosition.y;
        initialSpillColliderHeight = spillCollider.transform.localPosition.y;
        spillPrefab = GetComponentInChildren<OilDropletsSlpatOnCollision>();
    }
    
    private void Update()
    {
        // Silly random rotate cup
        if (rotateCupRandomly) {
            var val = rotateCupRandomPower;
            var rotX = UnityEngine.Random.Range(-val, val);
            var rotZ = UnityEngine.Random.Range(-val, val);
            var thing = new Vector3(rotX, 0, rotZ);
            transform.eulerAngles += thing;
        }

        var movementValue = 0f;
        if (useMouseInsteadOfOculus) {
            var mouseMovement = (lastMouse - Input.mousePosition).magnitude;
            movementValue = mouseMovement;
            lastMouse = Input.mousePosition;
        } else {
            movementValue = OVRInput.GetLocalControllerVelocity(controller).magnitude;
        }
        
        // Shake surface according to movement
        Vector3 surfaceShake = Vector3.zero;
        var toStabilize = oilSurfaceStabilizeRate;
        var moving = (movementValue > movementShakeThreshold);
        if (moving) {
            // Surface shake
            float val = 1;
            float rotX = UnityEngine.Random.Range(0f, 2*val) - val;
            float rotZ = UnityEngine.Random.Range(0f, 2*val) - val;
            surfaceShake = new Vector3(rotX, 0, rotZ) * -movementValue * movementShakeMultiplier;
            oilFakeSurface.transform.localEulerAngles += surfaceShake;
        }
        // Stabilize surface
        var surfaceRot = Quaternion.Lerp(oilFakeSurface.transform.localRotation, Quaternion.Euler(-transform.localEulerAngles), toStabilize);
        oilFakeSurface.transform.localRotation = surfaceRot;

        // Detect spill
        var liquidOut = spillCollider.touching.Contains(fakeSurfaceCollider.GetComponent<Collider>());
        var lost = oilFakeSurface.transform.localScale.y < spillLoseThreshold;
        var allSpilled = oilFakeSurface.transform.localScale.y <= 0.0f;
        var toSpill = liquidOut || lost;
        spillPrefab.SetPouringStatus(toSpill);
        if (toSpill)
        {
            var r = 1f;
            var p = point.transform.position - transform.position;
            var p2 = new Vector3(p.x, 0, p.z).normalized;
            var p3 = (r * p2);
            
            Vector3 spillPos;

            spillPos = p3;
            //  spillPos = fakeSurfaceCollider.transform.position + p3;
            //  spillPos = fakeSurfaceCollider.transform.TransformVector(p3);


            var thing = new GameObject();
            thing.transform.SetParent(fakeSurfaceCollider.transform);
            thing.transform.localPosition = spillPos;
            var pppos = thing.transform.position;
            Destroy(thing);
            spillPrefab.transform.position = pppos;
            spillPrefab.BeginPour();
            
            

            var oilFall = -oilSurfaceReduceRate * Time.deltaTime;
            oilFakeSurface.transform.localPosition = new Vector3
                (
                    oilFakeSurface.transform.localPosition.x,
                    Mathf.Max(0f, oilFakeSurface.transform.localPosition.y + oilFall),
                    oilFakeSurface.transform.localPosition.z
                );
        }
        
        var dist = (initialSpillColliderHeight - initialFakeSurfaceHeight);
        spillCollider.transform.localPosition = new Vector3
            (
                0,
                oilFakeSurface.transform.localPosition.y + dist - 0.05f,
                0
            );

        var thingVall = (oilFakeSurface.transform.localPosition.y) / initialFakeSurfaceHeight;
        var fixScale = thingVall;

        fixScale -= 0.1f;
        fixScale = Mathf.Max(0f, fixScale);
        oilFakeSurface.transform.localScale = new Vector3(fixScale, fixScale, fixScale);
    }
}
