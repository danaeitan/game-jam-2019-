using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    private const float shotDelay = 2.2f; //in seconds
    private float untilShotTimer;
    GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        prefab = Resources.Load("projectile") as GameObject;

    }
    // Update is called once per frame
    void Update()
    {
        untilShotTimer -= Time.deltaTime;
        var shootPressed = Input.GetKeyDown(KeyCode.Space);
        if (untilShotTimer <= 0 || shootPressed)
        {
            untilShotTimer = shotDelay;

            {
                GameObject projectile = Instantiate(prefab) as GameObject;
                projectile.transform.position = transform.position + Camera.main.transform.forward * 2;
                Rigidbody rb = projectile.GetComponent<Rigidbody>();
                rb.velocity = Camera.main.transform.forward * 40;
            }
        }
    }
}
