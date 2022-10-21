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
    public Button replayBtn;
    // public Button rotateBtn;
    private bool setupComplete = false;
    public bool playerOneTurn = true;
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
    public GameObject missTextPrefab;
    private List<GameObject> playerFires = new List<GameObject>();
    private List<GameObject> enemyFires = new List<GameObject>();
    private List<GameObject> missTexts = new List<GameObject>();
    public List<TileScript> allTileScripts;
    public Text topText;
    public Text playerShipText;
    public Text enemyShipText;

    // Start is called before the first frame update
    void Start()
    {  
        // This generates a random 5 index numbers.
        // This is used to give the player 5 random ships.
        while (playerOneShipIndex.Count <=4)
        {
            int newIndexValue = UnityEngine.Random.Range(0, 15);
            if (!playerOneShipIndex.Contains(newIndexValue)) 
            {
                playerOneShipIndex.Add(newIndexValue);
            }
        }
        // This gets the ships using the 5 random index numbers
        shipScript = ships[playerOneShipIndex[indexNum]].GetComponent<ShipScript>();
        // This makes it so that when you click on the next ship button it calls the method that moves onto the next ship
        nextBtn.onClick.AddListener(() => NextShipClicked());
        // rotateBtn.onClick.AddListener(() => RotateClicked());
        replayBtn.onClick.AddListener(() => ReplayClicked());
        enemyShips = enemyScript.PlaceEnemyShips();
    }

    // This is the method that moves onto the next ship
    private void NextShipClicked()
    {   
        // This checks if the current ship is properly on the board
        if (!shipScript.OnGameBoard())
        {
            // THis''ll make the ship flahs red
            shipScript.FlashColor(Color.red);
        }
        // This will happen if the current ship is placed properly
        else
        {
            // If the number of ships on the board is less than 5 then this will happen
            if (indexNum <= playerOneShipIndex.Count -2)
            {
                // This will move onto the next index number.
                indexNum++;
                // This will get the next ship
                shipScript = ships[playerOneShipIndex[indexNum]].GetComponent<ShipScript>();
                // This will make the ship flash blue
                shipScript.FlashColor(Color.blue);
            }
            // if theer are five ships on the bioard this will happen
            else
            {
                // The next ship button will dissapear. 
                nextBtn.gameObject.SetActive(false);
                // This will change the top text
                topText.text = "Guess dumb dummy dumbo";
                // This makes it so that the setup phase is over
                setupComplete = true;
                // This will make the ships dissapear
                for(int i = 0; i < ships.Length; i++)
                {
                    ships[i].SetActive(false);
                }
            }
        }
        
    }

    // This will be called when a tiled gets clicked and be called from the tile script.
    public void TileClicked(GameObject tile)
    {
        // If the setup phase is complete and it's the player turn this will happen
        if(setupComplete && playerOneTurn)
        {
            // These next four lines will shoot a missile at the tile clicked.
            Vector3 tilePos = tile.transform.position;
            tilePos.y += 15;
            playerOneTurn = false;
            Instantiate(missilePrefab, tilePos, missilePrefab.transform.rotation);
        }
        // If the setup phase isn't complete then this will happen.
        else if(!setupComplete)
        {
            // This will call the PlaceShip method and place a ship at the clicked tile.
            PlaceShip(tile);
            // This will call the SetClickedTile method with tile.
            shipScript.SetClickedTile(tile);
        }
    }

    // This method is used to place ships.
    private void PlaceShip(GameObject tile)
    {
        // This will get the current ship
        shipScript = ships[playerOneShipIndex[indexNum]].GetComponent<ShipScript>();
        // This clear the list of tiles the ship is touching
        shipScript.ClearTileList();
        // This will get the position the the ship is to be moved to.
        Vector3 newVec = shipScript.GetOffsetVec(tile.transform.position);
        // This will move the ship to the new location
        ships[playerOneShipIndex[indexNum]].transform.position = newVec;
    }

    // void RotateClicked()
    // {
    //     shipScript.RotateShip();
    // }

    // This method will check the place where the missile hits
    public void CheckHit(GameObject tile)
    {
        // This will get the number of the tile hit
        int tileNum = Int32.Parse(Regex.Match(tile.name, @"\d+").Value);
        // This will set the variable hitCount to zero
        int hitCount = 0;
        // This will loop for each tile that the enemy ships are on
        foreach(int[] tileNumArray in enemyShips)
        {
            // If the tile hit is one of the ones an enemy ship is on this will happen
            if(tileNumArray.Contains(tileNum))
            {
                // this will loop for a number of times equal to the length of the list that houses the coordinates of the enemy ships
                for(int i = 0; i < tileNumArray.Length; i++)
                {
                    // If the coordinate in the list with the index of i is equal to the tile hit this will happen
                    if(tileNumArray[i] == tileNum)
                    {
                        // This will set the coordinate to negative then increase the value of hirCount by one
                        tileNumArray[i] = -5;
                        hitCount++;
                    }
                    // if the coordinite in the list with the index of i is equal to negative five then it'll just increase the variable hitCount by one
                    else if(tileNumArray[i] == -5)
                    {
                        hitCount++;
                    }
                }
                // if the amount of hits is equal to the number of coordinates in the ship then this will happen
                if(hitCount == tileNumArray.Length)
                {
                    // This will increase the value of enemyShipCount by one
                    enemyShipCount--;
                    // This will display that the ship is sunk
                    topText.text = "Shunk";
                    // This will place a fire on the sunk ship and change the colour of the tile hit.
                    enemyFires.Add(Instantiate(firePrefab, tile.transform.position, Quaternion.identity));
                    tile.GetComponent<TileScript>().SetTileColor(1, new Color32(68, 0, 0, 255));
                    tile.GetComponent<TileScript>().SwitchColors(1);

                }
                // If the above is not true then this will happen
                else
                {
                    // This'll display a message that tells you that you hit then change the colour of the tile hit
                    topText.text = "Hutr";
                    enemyFires.Add(Instantiate(firePrefab, tile.transform.position, Quaternion.identity));
                    tile.GetComponent<TileScript>().SetTileColor(1, new Color32(255, 0, 0, 255));
                    tile.GetComponent<TileScript>().SwitchColors(1);
                }
                break;
            }
            
        }
        // If you miss then this will happen
        if(hitCount == 0)
        {
            // The tile will switch colors and then display a message saying you missed
            tile.GetComponent<TileScript>().SetTileColor(1, new Color32(38, 57, 76, 255));
            tile.GetComponent<TileScript>().SwitchColors(1); 
            missTexts.Add(Instantiate(missTextPrefab, tile.transform.position, Quaternion.identity));
            topText.text = "[insert lowtiergod speech here]";
        }
        // This will invoke the method that ends the players turn
        Invoke("EndPlayerTurn", 1.0f);
    }

    // This will be called when the enemy hits the player
    public void EnemyHitPlayer(Vector3 tile, int tileNum, GameObject hitObj)
    {
        // This will call the MissileHit method using the value of tileNum and have the enemy record that tile as hit
        enemyScript.MissileHit(tileNum);
        // this wll increase the y of the tile by 0.2 units
        tile.y += 0.2f;
        // This will place a fire on the tile hit
        playerFires.Add(Instantiate(firePrefab, tile, Quaternion.identity));
        // This will check if the the ship hit has been sank
        if(hitObj.GetComponent<ShipScript>().HitCheckSank())
        {
            // This will decrease the value of playerShipCount by one
            playerShipCount--;
            // This will set the displayed number of shops to equal the actual number of ships
            playerShipText.text = playerShipCount.ToString();
            // This will call the method that the enemy uses when it's sunk a player ship.
            enemyScript.SunkPlayer();
        }
        // This will end the enemies turn
        Debug.Log("Pot");
        Invoke("EndEnemyTurn", 2.0f);
    }

    // This method ends the players turn
    public void EndPlayerTurn()
    {
        // This will make all of the player's ship appear
        for (int i = 0; i < ships.Length; i++)
        {
            ships[i].SetActive(true);
        }
        // This will make all of the player fires appear
        foreach(GameObject fire in playerFires)
        {
            fire.SetActive(true);
        }
        // This will make all of the enemy fires dissapear
        foreach(GameObject fire in enemyFires)
        {
            fire.SetActive(false);
        }
        foreach(GameObject text in missTexts)
        {
            text.SetActive(false);
        }
        // This will make the displayed number of enemy ships equal to the number of enemy ships remaining.
        enemyShipText.text = "Enemy Ships:" + enemyShipCount.ToString();
        // This will set the top text to display that it's the enemy's turn
        topText.text = "Enemy's turn";
        // This will call the method that happens when it's enemies turn
        enemyScript.NPCTurn();
        // This will call the ColorAlltiles method
        ColorAllTiles(0);
        // If the players ship count is less than one then it displays that they lost.
        if(playerShipCount < 1)
        {
            GameOver("You lost bozo");
        }
    }

    //  This will hapen when the enemy's turn ends
    public void EndEnemyTurn()
    {
        // This will make all the player's ships dissapear
        for (int i = 0; i < ships.Length; i++)
        {
            ships[i].SetActive(false);
        }
        // This will make all the player's fires dissapear
        foreach(GameObject fire in playerFires)
        {
            fire.SetActive(false);
        }
        // This will make all of the enemy's appear
        foreach(GameObject fire in enemyFires)
        {
            fire.SetActive(true);
        }
        foreach(GameObject text in missTexts)
        {
            text.SetActive(true);
        }
        // This will set the displayed number of players ships to equal the number of player's ship existing
        playerShipText.text = "Player ships:" + playerShipCount.ToString();
        // This will change the top text to a promt telling the player to pick a tile
        topText.text = "piek a tiel";
        // this will make it the player's turn
        playerOneTurn = true;
        // This will call the ColorAllTiles method
        ColorAllTiles(1);
        // IF the number of enemy ships is less than one then it diplay's a message that tells the player that they win
        if(enemyShipCount < 1)
        {
            GameOver("You ein");
        }
    }

    // This is the ColorAllTiles method, it changes the color of tiles.
    private void ColorAllTiles(int colorIndex)
    {
        foreach (TileScript tileScript in allTileScripts)
        {
            tileScript.SwitchColors(colorIndex);
        }
    }

    // When the game is over this will happen
    void GameOver(string winner)
    {
        topText.text = "Game Over " + winner;
        replayBtn.gameObject.SetActive(true);
    }

    
    // This will restart the game.
    void ReplayClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
