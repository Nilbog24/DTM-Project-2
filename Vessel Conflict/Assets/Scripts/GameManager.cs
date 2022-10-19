using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] ships;
    private bool setupComplete = false;
    private bool playerOneTurn = true;
    public List<int> playerOneShipIndex;
    private int indexNum;
    public ShipScript shipScript; 
    // Start is called before the first frame update
    void Start()
    {  
        while (playerOneShipIndex.Count <=4) 
        {
            int indexNum = Random.Range(0, 16);
            if (!playerOneShipIndex.Contains(indexNum)) 
            {
                playerOneShipIndex.Add(indexNum);
            }
        }
        shipScript = ships[playerOneShipIndex[0]].GetComponent<ShipScript>();
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
        }
    }

    private void PlaceShip(GameObject tile)
    {
        shipScript = ships[playerOneShipIndex[0]].GetComponent<ShipScript>();
        shipScript.ClearTileList();
        Vector3 newVec = shipScript.GetOffsetVec(tile.transform.position);
        ships[playerOneShipIndex[0]].transform.position = newVec;

    }
}
