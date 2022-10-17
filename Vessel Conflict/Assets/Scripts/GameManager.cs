using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] ships;
    private bool setupComplete = false;
    private bool playerOneTurn = true;
    public int shipIndex = 0;
    // public ShipScript shipScript;
    // Start is called before the first frame update
    void Start()
    {
        // shipScript = ships[shipIndex].gameObject.GetComponent<ShipScript>();   
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

        ShipScript shipScript = ships[shipIndex].gameObject.GetComponent<ShipScript>();
        shipScript.ClearTileList();
        Vector3 newVec = shipScript.GetOffsetVec(tile.transform.position);
        ships[shipIndex].transform.localPosition = newVec;

    }
}
