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
        int tilesPerRow = 11;
        int maxSpecialTilesPerRow = 3;
        bool backToBack = false;

        for (int i = 0; i < 72; i++)
        {
            if (i > tilesPerRow)
            {
                maxSpecialTilesPerRow = 3;
                tilesPerRow += 12;
            }

            SpecialTile st = new SpecialTile();
            
            if (Random.Range(0, 6) < 3 && i > 3 && i < 70)
            {
                // this statement makes sure there are no back-to-back special tiles.
                if (backToBack == true)
                {
                    st.InitEmptyTile();
                    specialTiles[i] = st;
                    backToBack = false;
                }
                else
                {
                    if (maxSpecialTilesPerRow != 0)
                    {
                        st.InitializeTile(tileList[i].position.x, tileList[i].position.z);
                        specialTiles[i] = st;

                        maxSpecialTilesPerRow--;

                        backToBack = true;
                    }
                }
            }
            else
            {
                st.InitEmptyTile();
                specialTiles[i] = st;
                backToBack = false;
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