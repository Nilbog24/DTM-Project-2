using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    char[] guessGrid;
    List<int> potentialHits;
    List<int> currenthits;
    private int guess;
    public GameObject enemyMissilePrefab;
    private void Start()
    {
        potentialHits = new List<int>();
        currenthits = new List<int>();
        guessGrid = Enumerable.Repeat('o', 100).ToArray();
    }
    public List<int[]> PlaceEnemyShips()
    {
        List<int[]> enemyShips = new List<int[]>
        {
            new int[]{-1, -1, -1, -1, -1},
            new int[]{-1, -1, -1, -1},
            new int[]{-1, -1, -1},
            new int[]{-1, -1, -1},
            new int[]{-1, -1},
        };
        int[] gridNumbers = Enumerable.Range(1, 100).ToArray();
        bool taken = true;
        foreach(int[] tileNumArray in enemyShips)
        {
            taken = true;
            while(taken == true)
            {
                taken = false;
                int shipNose = UnityEngine.Random.Range(0, 99);
                int rotateBool = UnityEngine.Random.Range(0, 2);
                int minusAmount = rotateBool == 0 ? 10 : 1;
                for (int i = 0; i < tileNumArray.Length; i++)
                {
                    if((shipNose - (minusAmount * i)) < 0 || gridNumbers[shipNose - i * minusAmount] < 0)
                    {
                        taken = true;
                        break;
                    }
                    else if(minusAmount == 1 && shipNose/10 != ((shipNose - i * minusAmount)-1)/10)
                    {
                        taken = true;
                        break;
                    }
                }
                if (taken == false)
                {
                    for (int j =0; j < tileNumArray.Length; j++)
                    {
                        tileNumArray[j] = gridNumbers[shipNose - j * minusAmount];
                        gridNumbers[shipNose - j * minusAmount] = -1;
                    }
                }
            }
        }
        foreach (int[] numArray in enemyShips)
        {
            string temp ="";
            for (int i = 0; i < numArray.Length; i++)
            {
                temp += ", " + numArray[i];
            }
            Debug.Log(temp);
        }
        return enemyShips;
    }

    public void NPCTurn()
    {
        List<int> hitIndex = new List<int>();
        for(int i = 0; i < guessGrid.Length; i++)
        {
            if(guessGrid[i] == 'h')
            {
                hitIndex.Add(i);
            }
        }
        if(hitIndex.Count > 1)
        {
            int diff = hitIndex[1] - hitIndex[0];
            int posNeg = Random.Range(0, 2)*2 - 1;
            int nextIndex = hitIndex[0] + diff;
            while(guessGrid[nextIndex] != 'o')
            {
                if(guessGrid[nextIndex] == 'm' || nextIndex > 100 || nextIndex < 0)
                {
                    diff *= -1;
                }
                nextIndex += diff;
            }
            guess = nextIndex;
        }
        else if (hitIndex.Count == 1)
        {
            List<int> closeTiles = new List<int>();
            closeTiles.Add(1);
            closeTiles.Add(-1);
            closeTiles.Add(10);
            closeTiles.Add(-10);
            int index = Random.Range(0, closeTiles.Count);
            int possibleGuess = hitIndex[0] + closeTiles[index];
            bool onGrid = possibleGuess > -1 && possibleGuess < 100;
            while((!onGrid || guessGrid[possibleGuess] != 'o') && closeTiles.Count > 0)
            {
                closeTiles.RemoveAt(index);
                index = Random.Range(0, closeTiles.Count);
                possibleGuess = hitIndex[0] + closeTiles[index];
                onGrid = possibleGuess > -1 && possibleGuess < 100;
            }
            guess = possibleGuess;
        }
        else
        {
            int nextIndex = Random.Range(0, 100);
            while(guessGrid[nextIndex] != 'o')
            {
                nextIndex = Random.Range(0, 100);
            }
            guess =  nextIndex;
        }
        GameObject tile = GameObject.Find("Tile (" + (guess + 1) + ")");
        guessGrid[guess] = 'm';
        Vector3 vec = tile.transform.position; 
        vec.y += 15;
        GameObject missile = Instantiate(enemyMissilePrefab, vec, enemyMissilePrefab.transform.rotation);
        missile.GetComponent<EnemyMissileScript>().SetTarget(guess);
        missile.GetComponent<EnemyMissileScript>().targetTileLocation = tile.transform.position;
    }

    public void MissileHit(int hit)
    {
        guessGrid[guess] = 'h';
    }

    public void SunkPlayer()
    {
        for(int i = 0; i < guessGrid.Length; i++)
        {
            if (guessGrid[i] == 'h') guessGrid[i] = 'x';
        }
    }
}
