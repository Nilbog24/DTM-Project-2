using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] ships;
    public Button nextBtn;
    // public Button rotateBtn;
    private bool setupComplete = false;
    private bool playerOneTurn = true;
    public List<int> playerOneShipIndex;
    private int newIndexValue;
    private int indexNum;
    public ShipScript shipScript; 
    public EnemyScript enemyScript;
    public GameObject missilePrefab;
    public GameObject enemyMissilePrefab;
    List<int[]> enemyShips;

    private int enemyShipCount = 5;
    private int playerShipCount = 5;
    public Text topText;

    // Start is called before the first frame update
    void Start()
    {  
        while (playerOneShipIndex.Count <=4)
        {
            int newIndexValue = Random.Range(0, 16);
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
        if (indexNum <= playerOneShipIndex.Count -2)
        {
            indexNum++;
            shipScript = ships[playerOneShipIndex[indexNum]].GetComponent<ShipScript>();
        }else
        {
            enemyScript.PlaceEnemyShips();
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
        int tileNum = Int32.Parse(Regex.Match(tile.name, @"/d+").Value);
        int hitCount = 0;
        foreach(int tileNumArray in enemyShips)
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
}
