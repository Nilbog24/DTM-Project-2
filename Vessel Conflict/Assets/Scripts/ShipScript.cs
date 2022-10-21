using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    public List<GameObject> touchTiles = new List<GameObject>();
    public float xOffset = 0f;
    public float zOffset = 0f;
    // private float nextYRotation = 90f;
    private GameObject clickedTile;
    int hitCount = 0;
    public int shipSize;
    private Material[] allMaterials;
    List<Color> allColors= new List<Color>();

    // If a colliding object is a tile then this method will add it to the list of tiles the ship is touching.
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Tile"))
        {
            touchTiles.Add(collision.gameObject);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        // This if statement gets all of the ships that are slightly off in the z axis and sets their z offset to -0.5 units
        if (gameObject.name == "Ship EGGE" || gameObject.name == "Ship ECCE Z1" || 
        gameObject.name == "Ship ECMCE Z1" || gameObject.name == "Ship ECMCE Z2" || 
        gameObject.name == "Ship EE" || gameObject.name == "Ship EGMGE" || 
        gameObject.name == "Ship EMCE 2" || gameObject.name == "Ship EMCGE" || 
        gameObject.name == "Ship EME" || gameObject.name == "Ship EMGE") {
            zOffset = -0.5f;
        }
        // This if statement gets all of the ships that are slightly off in the x axis and sets their x offset to 0.5 units
        if (gameObject.name == "Ship ECCE Z2" || gameObject.name == "Ship ECMCE Z1" || 
        gameObject.name == "Ship ECMCE Z2" || gameObject.name == "Ship ECMCE" || 
        gameObject.name == "Ship EMCE" || gameObject.name == "Ship EMCGE" || 
        gameObject.name == "Ship EGMGE" || gameObject.name == "Ship EME") {
            xOffset = 0.5f;
        }

        //  This gets all of the renderer components from the materials
        allMaterials = GetComponent<Renderer>().materials;
        // For each of the materials this for loop will add it's colour to a list of colours 
        for(int i = 0; i < allMaterials.Length; i++)
        {
            allColors.Add(allMaterials[i].color);
        }
    }

    // This method clears the list that holds all the tiles the ship is touching
    public void ClearTileList()
    {
        touchTiles.Clear();
    }

    // This method uses the position of a tile to get the position that the ship is going to be placed to.
    public Vector3 GetOffsetVec(Vector3 tilePos)
    { 
        return new Vector3(tilePos.x + xOffset, 2, tilePos.z + zOffset);
    }

    // public void RotateShip()
    // {
    //     if(clickedTile == null)
    //     {
    //         return;
    //     }
    //     touchTiles.Clear();
    //     transform.localEulerAngles += new Vector3(0, nextYRotation, 0);
    //     nextYRotation += 90;
    //     if (nextYRotation == 360)
    //     {
    //         nextYRotation = 90;
    //     }
    //     float temp = xOffset;
    //     xOffset = zOffset;
    //     zOffset = temp;
    //     SetPosition(clickedTile.transform.position);
    // }

    // This method first clears the tile list and then moves the ship to a new position.
    public void SetPosition(Vector3 newVec)
    {
        ClearTileList();
        transform.localPosition = new Vector3(newVec.x + xOffset, 2, newVec.z + zOffset);
    }

    // This method sets a clicked tile to the value of the variable clickedTile
    public void SetClickedTile(GameObject tile)
    {
        clickedTile = tile;
    }

    // This method returns a value of true if the number of tiles a ship is touching is equal the amount of tiles the ship takes up.
    public bool OnGameBoard()
    {
        return touchTiles.Count == shipSize;
    }

    // This method first increases the value of hitCount by one and then will return a value of true if the number of hits is greater or equal to the size of the ship
    public bool HitCheckSank()
    {
        hitCount++;
        return shipSize <= hitCount;
    }

    // This method will make the ship turn into a different colour for a bit before invoking the ResetColor method
    public void FlashColor(Color tempColor)
    {
        foreach (Material mat in allMaterials)
        {
            mat.color = tempColor;
        }
        Invoke("ResetColor", 0.5f);
    }

    // This method resets the color fo the ship
    private void ResetColor()
    {
        int i = 0;
        foreach(Material mat in allMaterials)
        {
            mat.color = allColors[i++];
        }
    }
}
