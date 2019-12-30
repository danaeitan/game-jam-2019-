using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private float shotDelay = 2.2f; //in seconds
    [SerializeField]
    private float shotSpeed = 40f;


    private float untilShotTimer = 5f;

    // Start is called before the first frame update
    void Start()
    {
        untilShotTimer = shotDelay;
        //InvokeRepeating("ShootProjectile");
    }

    // Update is called once per frame
    void Update()
    {
        untilShotTimer -= Time.deltaTime;
        var shootPressed = Input.GetKeyDown(KeyCode.Space);
        if (untilShotTimer <= 0 || shootPressed) {
            untilShotTimer = shotDelay;
            // Shoot new prefab
            GameObject projectile = Instantiate(prefab) as GameObject;
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            var toTarget = (Camera.main.transform.position - transform.position).normalized;
            //set initial shot position and velocity
            projectile.transform.position = transform.position;
            rb.velocity = toTarget * shotSpeed;
            //TODo: Destroy projectile after shot
        }
    }
}
