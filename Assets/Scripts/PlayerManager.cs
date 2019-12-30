using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private GameManager gameManager;
    private int collisionCount;
 
    public string enemyTag = "";

    // Start is ca
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            print("collided with a wall");
            gameManager.OnRestartLevel();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("You've been hit!! " + collisionCount);
            if (collisionCount > 2)
            {
                gameManager.OnRestartLevel();
            }
            collisionCount++;
        }
    }
}
