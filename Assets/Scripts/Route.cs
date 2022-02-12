using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    public List<Transform> tileList = new List<Transform>();

    private Transform[] tiles;
    private SpecialTile[] specialTiles = new SpecialTile[72];

    private void Start()
    {
        FillRoute();
        RandomizeSpecialTiles();
    }

    private void RandomizeSpecialTiles()
    {
        int tilesPerRow = 12;
        int maxSpecialTilesPerRow = 3;

        for (int i = 0; i < 72; i++)
        {
            SpecialTile st = new SpecialTile();
            
            if (Random.Range(0, 6) < 2 && i > 3 && i < 70)
            {
                if (maxSpecialTilesPerRow != 0)
                {
                    st.InitializeTile(tileList[i].position.x, tileList[i].position.z);
                    specialTiles[i] = st;

                    maxSpecialTilesPerRow--;
                }

                if (i + 1 >= tilesPerRow)
                {
                    maxSpecialTilesPerRow = 3;
                    tilesPerRow += tilesPerRow;
                }

            }
            else
            {
                st.InitEmptyTile();
                specialTiles[i] = st;
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

    /*private SpecialTile GetSpecialTile(int index)
    {
        return specialTiles[index];
    }*/

    public SpecialTile[] GetSpecialTiles()
    {
        return specialTiles;
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