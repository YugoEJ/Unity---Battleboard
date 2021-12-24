using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    Transform[] tiles;
    //public GameObject[] specialTiles;
    SpecialTile[] specialTiles = new SpecialTile[72];

    public List<Transform> tileList = new List<Transform>();

    // String[] specialTileArray;
    // here we will create an array of Strings that will hold the names of the special tiles (powerups/minigames/other effects)

    private void Start()
    {
        FillRoute();
        RandomizeSpecialTiles();
    }

    private void RandomizeSpecialTiles()
    {
        for (int i = 0; i < 72; i++)
        {
            SpecialTile sp = new SpecialTile();
            
            if (Random.Range(0, 10) == 1)
            {
                sp.InitializeTile(tileList[i].position.x, tileList[i].position.z);
                specialTiles[i] = sp;
            }
        }
    }

    private void FillRoute()
    {
        tileList.Clear();

        tiles = GetComponentsInChildren<Transform>();

        foreach (Transform tile in tiles)
        {
            if (tile != this.transform)
            {
                tileList.Add(tile);
            }
        }
    }

        /*private void OnDrawGizmos()
        {
        Gizmos.color = Color.red;

        FillNodes();

            for (int i = 0; i < childObjectList.Count; i++)
            {
                Vector3 currentPos = childObjectList[i].position;

                if (i > 0)
                {
                    Vector3 prevPos = childObjectList[i - 1].position;
                    Gizmos.DrawLine(prevPos, currentPos);
                }

                // we need to randomize the tiles (somewhere in this file) by using the String array, with some logic to spread them out a bit on the board
            }
        }*/
}