using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    GameManager gameManager;
    Ray ray;
    RaycastHit hit;
    private bool missileHit = false;
    Color32[] hitColor = new Color32[2];
    // Start is called before the first frame update
    void Start()
    {
        // This will get the game manager script
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // These will get the two hit colours
        hitColor[0] = gameObject.GetComponent<MeshRenderer>().material.color;
        hitColor[1] = gameObject.GetComponent<MeshRenderer>().material.color;

    }

    // Update is called once per frame
    void Update()
    {   
        // Everything inside of this if statement gets used to get the thing the player clicked.
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            if(Input.GetMouseButtonDown(0) && hit.collider.gameObject.name == gameObject.name)
            {
                if(missileHit == false)
                {
                    gameManager.TileClicked(hit.collider.gameObject);
                }
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Missile"))
        {
            missileHit = true;
        }
        else if(collision.gameObject.CompareTag("EnemyMissile"))
        {
            hitColor[0] = new Color32(38, 57, 76, 255);
        }
    }

    public void SetTileColor(int index, Color32 color)
    {
        hitColor[index] = color;
    }

    public void SwitchColors(int colorIndex)
    {
        GetComponent<Renderer>().material.color = hitColor[colorIndex];
    }
}
