using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // These two variables are used to determine which players turn it is.
    // There are two variables's here instead of one that switches between two different states.
    // This set up works better with the rest of the code.
    public bool PlayerOneTurn = false; 
    public bool PlayerTwoTurn = false;
    // These two variables are used to determine the amount of ships remaining on each side.
    public int PlayerOneShips;
    public int PlayerTwoShips;
    // This variable is used when determining if the player is alone or not.
    public int GameMode;
    // This is used when the game is in two player mdoe to determine who starts.
    private int WhoStarts;

    // Update is called once per frame
    void Update()
    {  
        // If the the game is in twoplayer mode then this will run
        if(GameMode == 2){
            // This will reset the number of ships on both side to five
            PlayerOneShips = 5;
            PlayerTwoShips = 5;
            // This will decide who starts
            WhoStarts = Random.Range(1,2);
            //There will be code here to trigger a coin flip animation later.

            if (WhoStarts == 1){
                PlayerOneTurn = true;
            }

            if (WhoStarts == 2){
                PlayerTwoTurn = true;
            }

        }
            
           
    }
}
