using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] ships;
    public Button nextBtn;
    // public Button rotateBtn;
    private bool setupComplete = false;
    private bool playerOneTurn = true;
    public List<int> playerOneShipIndex;
    private int newIndexValue;
    public int indexNum;
    public ShipScript shipScript; 
    public EnemyScript enemyScript;
    public GameObject missilePrefab;
    public GameObject enemyMissilePrefab;
    List<int[]> enemyShips;

    private int enemyShipCount = 5;
    private int playerShipCount = 5;
    public GameObject firePrefab;
    private List<GameObject> playerFires;
    public Text topText;
    public Text playerShipText;
    public Text enemyShipText;

    // Start is called before the first frame update
    void Start()
    {  
        while (playerOneShipIndex.Count <=4)
        {
            int newIndexValue = UnityEngine.Random.Range(0, 16);
            if (!playerOneShipIndex.Contains(newIndexValue)) 
            {
                playerOneShipIndex.Add(newIndexValue);
            }
        }
        shipScript = ships[playerOneShipIndex[indexNum]].GetComponent<ShipScript>();
        nextBtn.onClick.AddListener(() => NextShipClicked());
        // rotateBtn.onClick.AddListener(() => RotateClicked());
        enemyShips = enemyScript.PlaceEnemyShips();
    }

    private void NextShipClicked()
    {
        if (!shipScript.OnGameBoard())
        {
            shipScript.FlashColor(Color.red);
        }
        else
        {
            if (indexNum <= playerOneShipIndex.Count -2)
            {
                indexNum++;
                shipScript = ships[playerOneShipIndex[indexNum]].GetComponent<ShipScript>();
                shipScript.FlashColor(Color.yellow);
            }
            else
            {
                nextBtn.gameObject.SetActive(false);
                topText.text = "Guess dumb dummy dumbo";
                setupComplete = true;
                for(int i = 0; i < ships.Length; i++)
                {
                    ships[i].SetActive(false);
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {  
        
    }

    public void TileClicked(GameObject tile)
    {
        if(setupComplete && playerOneTurn)
        {
            // SHooting?
        }
        else if(!setupComplete)
        {
            PlaceShip(tile);
            shipScript.SetClickedTile(tile);
        }
    }

    private void PlaceShip(GameObject tile)
    {
        shipScript = ships[playerOneShipIndex[indexNum]].GetComponent<ShipScript>();
        shipScript.ClearTileList();
        Vector3 newVec = shipScript.GetOffsetVec(tile.transform.position);
        ships[playerOneShipIndex[indexNum]].transform.position = newVec;
    }

    // void RotateClicked()
    // {
    //     shipScript.RotateShip();
    // }

    public void CheckHit(GameObject tile)
    {
        int tileNum = Int32.Parse(Regex.Match(tile.name, @"\d+").Value);
        int hitCount = 0;
        foreach(int[] tileNumArray in enemyShips)
        {
            if(tileNumArray.Contains(tileNum))
            {
                for(int i = 0; i < tileNumArray.Length; i++)
                {
                    if(tileNumArray[i] == tileNum)
                    {
                        tileNumArray[i] = -5;
                        hitCount++;
                    }
                    else if(tileNumArray[i] == -5)
                    {
                        hitCount++;
                    }
                }
                if(hitCount == tileNumArray.Length)
                {
                    enemyShipCount--;
                    topText.text = "Shunk";
                    // enemy fires
                    // color
                }
                else
                {
                    topText.text = "Hutr";
                    // color
                }
                break;
            }
            
        }
        if(hitCount == 0)
        {
            // color
            topText.text = "[insert lowtiergod speech here]";
        }
        // Invoke("EndPlayerTurn", 1.0f);
    }

    public void EnemyHitPlayer(Vector3 tile, int tileNum, GameObject hitObj)
    {
        enemyScript.MissileHit(tileNum);
        tile.y += 0.2f;
        playerFires.Add(Instantiate(firePrefab, tile, Quaternion.identity));
        if(hitObj.GetComponent<ShipScript>().HitCheckSank())
        {
            playerShipCount--;
            playerShipText.text = playerShipCount.ToString();
            enemyScript.SunkPlayer();
        }
        // Invoke("EndEnemyTurn", 2.0f);
    }
}
