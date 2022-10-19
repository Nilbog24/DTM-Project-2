using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] ships;
    public Button nextBtn;
    public Button rotateBtn;
    private bool setupComplete = false;
    private bool playerOneTurn = true;
    public List<int> playerOneShipIndex;
    private int newIndexValue;
    private int indexNum;
    public ShipScript shipScript; 
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
    }

    private void NextShipClicked()
    {
        if (indexNum <= playerOneShipIndex.Count -2)
        {
            indexNum++;
            shipScript = ships[playerOneShipIndex[indexNum]].GetComponent<ShipScript>();
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
        }
    }

    private void PlaceShip(GameObject tile)
    {
        shipScript = ships[playerOneShipIndex[indexNum]].GetComponent<ShipScript>();
        shipScript.ClearTileList();
        Vector3 newVec = shipScript.GetOffsetVec(tile.transform.position);
        ships[playerOneShipIndex[indexNum]].transform.position = newVec;
    }
}
