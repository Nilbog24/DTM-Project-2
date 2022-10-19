using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    List<GameObject> touchTiles = new List<GameObject>();
    public float xOffset = 0f;
    public float zOffset = 0f;


    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name == "Ship EGGE" || gameObject.name == "Ship ECCE Z1" || 
        gameObject.name == "Ship ECMCE Z1" || gameObject.name == "Ship ECMCE Z2" || 
        gameObject.name == "Ship EE" || gameObject.name == "Ship EGMGE" || 
        gameObject.name == "Ship EMCE 2" || gameObject.name == "Ship EMCGE" || 
        gameObject.name == "Ship EME" || gameObject.name == "Ship EMGE") {
            zOffset = -0.5f;
        }
        if (gameObject.name == "Ship ECCE Z2" || gameObject.name == "Ship ECMCE Z1" || 
        gameObject.name == "Ship ECMCE Z2" || gameObject.name == "Ship ECMCE" || 
        gameObject.name == "Ship EMCE" || gameObject.name == "Ship EMCGE" || 
        gameObject.name == "Ship EGMGE" || gameObject.name == "Ship EME") {
            xOffset = 0.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearTileList()
    {
        touchTiles.Clear();
    }

    public Vector3 GetOffsetVec(Vector3 tilePos)
    {
        return new Vector3(tilePos.x + xOffset, 2, tilePos.z + zOffset);
    }
}
