using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileScript : MonoBehaviour
{
    GameManager gameManager;
    EnemyScript enemyScript;
    public Vector3 targetTileLocation;
    private int targetTile = -1;
    // Start is called before the first frame update
    void Start()
    {
        // These two lines will grab the GameManager and EnemyScript scripts and put them into their respective variables
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyScript = GameObject.Find("Enemy").GetComponent<EnemyScript>();
    }
    
    // This method will run when the missile collides withs something
    private void OnCollisionEnter(Collision collision)
    {
        // If it collides with a ship then it'll sun the EnemyHitPLayer method.
        if(collision.gameObject.CompareTag("Ship"))
        {
            gameManager.EnemyHitPlayer(targetTileLocation, targetTile, collision.gameObject);
        }
        // If it collides with anything else then it'll run the PauseAndEnd method
        else
        {
            enemyScript.PauseAndEnd(targetTile);
        }
        // Then it'll destroy itself
        Destroy(gameObject);
    }

    // This method sets the target value that is given when this method is called and sets it as targetTile
    public void SetTarget(int target)
    {
        targetTile = target;
    }
}
