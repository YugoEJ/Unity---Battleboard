using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Route currentRoute;

    //private int stepsToTake;
    private int routePos;
    private bool isMoving;

    public bool CanMove(int stepsToTake)
    {
        if ((routePos + stepsToTake) < (currentRoute.tileList.Count - 1))                // if the amount of steps to take does not overflow, move player piece
        {
            return true;
        }
        else
        {
            return false;
            // winner
        }
    }

    // IsEdgePos() checks (for moving animation) whether to calculate for X or Z axes
    public bool IsEdgePos(int currNode)
    {
        bool isEdgePos = false;

        while (currNode > 0)
        {
            if (currNode - 12 == 11 || currNode == 11)
            {
                isEdgePos = true;
                break;
            }

            currNode -= 12;
        }

        return isEdgePos;
    }

    public bool IsMoving()
    {
        return this.isMoving;
    }

    public void SetMoving(bool moving)
    {
        this.isMoving = moving;
    }

    /*public int StepsToTake()
    {
        return stepsToTake;
    }*/

    public int RoutePos()
    {
        return this.routePos;
    }

    public void SetRoutePos(int newPos)
    {
        this.routePos = newPos;
    } 

    public bool IsOnSpecialTile()
    {
        return (currentRoute.GetSpecialTiles()[routePos].GetTileEffect() == "effect");
    }
}