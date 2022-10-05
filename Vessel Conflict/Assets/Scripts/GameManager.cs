using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] ships;
    private bool setupComplete = false;
    private bool playerOneTurn = true;

    // Start is called before the first frame update
    void Start()
    {
         
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
    {}
}
