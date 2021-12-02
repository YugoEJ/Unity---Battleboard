using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    Transform[] childObjects;

    public List<Transform> childObjectList = new List<Transform>();

    // String[] specialTileArray;
    // here we will create an array of Strings that will hold the names of the special tiles (powerups/minigames/other effects)

    private void Start()
    {
        // when OnDrawGizmos() is removed (because it's only for visual debugging), FillNodes() should be activated in Start().
        // FillNodes();
    }

    private void OnDrawGizmos()
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

            // here we will randomize the tiles by using the String array, with some logic to spread them out a bit on the board
        }
    }

    void FillNodes()
    {
        childObjectList.Clear();

        childObjects = GetComponentsInChildren<Transform>();

        foreach (Transform child in childObjects)
        {
            if (child != this.transform)
            {
                childObjectList.Add(child);
            }
        }
    }

}
