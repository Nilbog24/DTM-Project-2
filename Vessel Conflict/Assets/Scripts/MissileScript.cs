using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    // This variable wi;; store the game  manager script 
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        // This will put the GameManager script into the gameManager object
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // This method will make it so that when this collides with something it'll run the CheckHit method and then the object will destroy itself
    private void OnCollisionEnter(Collision collision)
    {
        gameManager.CheckHit(collision.gameObject);
        Destroy(gameObject);
    }
}
